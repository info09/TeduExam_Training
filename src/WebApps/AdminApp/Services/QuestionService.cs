using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminApp.Services.Interfaces;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.WebUtilities;

namespace AdminApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;

        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedList<QuestionDto>> GetQuestionsPagingAsync(QuestionSearch questionSearch)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["pageIndex"] = questionSearch.PageNumber.ToString(),
                ["pageSize"] = questionSearch.PageSize.ToString()
            };

            if(!string.IsNullOrEmpty(questionSearch.Content))
                queryParams.Add("seachKeyword", questionSearch.Content);

            string url = QueryHelpers.AddQueryString("/api/v1/Questions", queryParams);

            var result = await _httpClient.GetFromJsonAsync<PagedList<QuestionDto>>(url);
            return result;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<QuestionDto>($"/api/v1/questions/{id}");
            return result;
        }

        public async Task<bool> CreateAsync(CreateQuestionRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/v1/questions", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateQuestionRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/v1/questions", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/v1/questions/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
