using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using System.Threading.Tasks;

namespace PortalApp.Services.Interface
{
    public interface IExamService
    {
        Task<ApiResult<PagedList<ExamDto>>> GetExamPagingAsync(ExamSearch search);

        Task<ApiResult<ExamDto>> GetExamByIdAsync(string id);
    }
}
