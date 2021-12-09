

using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdminApp.Core;
using AdminApp.Core.Authentication;
using AdminApp.Models;
using AdminApp.Services.Interfaces;
using Blazored.SessionStorage;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdminApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        private static DiscoveryDocumentResponse _disco;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, ISessionStorageService sessionStorageService, IConfiguration configuration, ILogger<AuthService> logger, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
            _configuration = configuration;
            _logger = logger;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<TokenResponse> Login(LoginRequest request)
        {
            _disco = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(
                _httpClient,
                _configuration["IdentityServerConfig:IdentityServerUrl"]);

            if (_disco.IsError)
            {
                throw new ApplicationException($"Status code: {_disco.IsError}, Error: {_disco.Error}");
            }

            var token = await RequestTokenAsync(request.UserName, request.Password);
            if (token.IsError == false)
            {
                await _sessionStorageService.SetItemAsync(KeyConstants.AccessToken, token.AccessToken);
                await _sessionStorageService.SetItemAsync(KeyConstants.RefreshToken, token.RefreshToken);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(request.UserName);
            }

            return token;
        }

        public async Task Logout()
        {
            await _sessionStorageService.RemoveItemAsync(KeyConstants.AccessToken);
            await _sessionStorageService.RemoveItemAsync(KeyConstants.RefreshToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<TokenResponse> RequestTokenAsync(string user, string password)
        {
            _logger.LogInformation("begin RequestTokenAsync");
            var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = _disco.TokenEndpoint,

                ClientId = _configuration["IdentityServerConfig:ClientId"],
                ClientSecret = _configuration["IdentityServerConfig:ClientSecret"],
                Scope = "email openid roles profile offline_access",

                UserName = user,
                Password = password
            });

            return response;
        }
    }
}
