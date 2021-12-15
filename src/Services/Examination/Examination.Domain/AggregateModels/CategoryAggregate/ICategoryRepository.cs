using Examination.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examination.Shared.SeedWork;

namespace Examination.Domain.AggregateModels.CategoryAggregate
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<PagedList<Category>> GetCategoriesPagingAsync(string searchKeyword, int pageIndex, int pageSize);

        Task<Category> GetCategoryByIdAsync(string id);

        Task<Category> GetCategoryByNameAsync(string name);

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
