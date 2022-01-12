using AdminApp.Services.Interfaces;
using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class ExamService : IExamService
    {
        private readonly HttpClient _httpClient;

        public ExamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<PagedList<ExamDto>>> GetExamsPagingAsync(ExamSearch search)
        {
            var queryParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(search.Name))
                queryParam.Add("searchKeyword", search.Name);

            if (!string.IsNullOrEmpty(search.CategoryId))
                queryParam.Add("categoryId", search.CategoryId);

            string url = QueryHelpers.AddQueryString("/api/v1/Exams/paging", queryParam);

            var result = await _httpClient.GetFromJsonAsync<ApiResult<PagedList<ExamDto>>>(url);
            return result;
        }

        public async Task<ApiResult<ExamDto>> GetExamByIdAsync(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResult<ExamDto>>($"/api/v1/Exams/{id}");
            return result;
        }

        public async Task<ApiResult<ExamDto>> CreateAsync(CreateExamRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/v1/exams", request);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResult<ExamDto>>(content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ApiResult<bool>> UpdateAsync(UpdateExamRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/v1/Exams", request);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResult<bool>>(content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/v1/Exams/{id}");
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResult<bool>>(content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}