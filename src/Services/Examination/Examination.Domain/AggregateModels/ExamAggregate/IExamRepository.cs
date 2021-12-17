using Examination.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examination.Shared.SeedWork;

namespace Examination.Domain.AggregateModels.ExamAggregate
{
    public interface IExamRepository : IRepositoryBase<Exam>
    {
        Task<IEnumerable<Exam>> GetAllExamListAsync();
        Task<Exam> GetExamByIdAsync(string id);
        Task<PagedList<Exam>> GetExamsPagingAsync(string categoryId, string searchKeyword, int pageIndex, int pageSize);
    }
}
