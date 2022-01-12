using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using PortalApp.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PortalApp.Services
{
    public class ExamService : BaseService, IExamService
    {
        public ExamService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<ApiResult<PagedList<ExamDto>>> GetExamPagingAsync(ExamSearch search)
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
            var result = await GetAsync<PagedList<ExamDto>>(url, true);
            return result;
        }

        public async Task<ApiResult<ExamDto>> GetExamByIdAsync(string id)
        {
            return await GetAsync<ExamDto>($"/api/v1/Exams/{id}", true);
        }
    }
}
