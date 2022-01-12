using System.Threading.Tasks;
using Examination.Shared.Exams;
using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalApp.Services.Interface;

namespace PortalApp.Pages.Exams
{
    [Authorize]
    public class ExamListModel : PageModel
    {
        private readonly IExamService _examService;

        [BindProperty] public PagedList<ExamDto> Exam { get; set; }

        public ExamListModel(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] ExamSearch search)
        {
            var result = await _examService.GetExamPagingAsync(search);

            if (result.IsSuccessed)
            {
                Exam = result.ResultObj;
            }

            return Page();
        }
    }
}
