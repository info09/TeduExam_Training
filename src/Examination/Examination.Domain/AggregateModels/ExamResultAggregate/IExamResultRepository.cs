using Examination.Domain.SeedWork;
using System.Threading.Tasks;

namespace Examination.Domain.AggregateModels.ExamResultAggregate
{
    public interface IExamResultRepository : IRepositoryBase<ExamResult>
    {
        Task<ExamResult> GetDetails(string userId, string examId);
    }
}
