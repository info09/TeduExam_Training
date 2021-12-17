using System.Collections.Generic;
using Examination.Domain.SeedWork;
using Examination.Shared.SeedWork;
using System.Threading.Tasks;
using Examination.Shared.Enum;

namespace Examination.Domain.AggregateModels.QuestionAggregate
{
    public interface IQuestionRepository : IRepositoryBase<Question>
    {
        Task<PagedList<Question>> GetQuestionsPagingAsync(string categoryId, string searchKeyword, int pageIndex, int pageSize);

        Task<Question> GetQuestionByIdAsync(string id);

        Task<Question> GetQuestionByContentAsync(string content);

        Task<List<Question>> GetRandomQuestionsForExamAsync(string categoryId, Level level, int numberOfQuestion);
    }
}
