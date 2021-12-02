using Examination.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination.Domain.AggregateModels.ExamAggregate
{
    public interface IExamRepository : IRepositoryBase<Exam>
    {
        Task<IEnumerable<Exam>> GetExamListAsync();
        Task<Exam> GetExamByIdAsync(string id);
    }
}
