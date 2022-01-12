using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.Http;
using PortalApp.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PortalApp.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<CategoryDto>>> GetCategoriesAsync()
        {
            var result = await GetAsync<List<CategoryDto>>("/api/v1/categories");
            return result;
        }
    }
}
