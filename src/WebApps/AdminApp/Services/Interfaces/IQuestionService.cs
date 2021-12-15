using System.Threading.Tasks;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;

namespace AdminApp.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<PagedList<QuestionDto>> GetQuestionsPagingAsync(QuestionSearch questionSearch);

        Task<QuestionDto> GetQuestionByIdAsync(string id);

        Task<bool> CreateAsync(CreateQuestionRequest request);

        Task<bool> UpdateAsync(UpdateQuestionRequest request);

        Task<bool> DeleteAsync(string id);
    }
}
