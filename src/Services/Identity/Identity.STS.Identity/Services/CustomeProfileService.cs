using Identity.Admin.EntityFramework.Shared.Entities.Identity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.STS.Identity.Services
{
    public class CustomeProfileService : IProfileService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ILogger<CustomeProfileService> _logger;

        public CustomeProfileService(UserManager<UserIdentity> userManager, ILogger<CustomeProfileService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            _logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                context.Subject.GetSubjectId(),
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            var claimsFromDb = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("role", string.Join(";",roles)),
                new Claim("username", user.UserName),
                new Claim("email", user.Email),

            };
            claims.AddRange(claimsFromDb);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
