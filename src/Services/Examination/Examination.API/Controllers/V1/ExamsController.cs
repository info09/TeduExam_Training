using System.Threading.Tasks;
using Examination.Application.Commands.V1.Exams.CreateExam;
using Examination.Application.Commands.V1.Exams.DeleteExam;
using Examination.Application.Commands.V1.Exams.UpdateExam;
using Examination.Application.Queries.V1.Exams.GetAllExams;
using Examination.Application.Queries.V1.Exams.GetExamById;
using Examination.Application.Queries.V1.Exams.GetExamsPaging;
using Examination.Shared.Exams;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Examination.API.Controllers.V1
{
    public class ExamsController : BasesController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExamsController> _logger;

        public ExamsController(IMediator mediator, ILogger<ExamsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamListAsync()
        {
            _logger.LogInformation("BEGIN: GetExamList");
            var query = new GetAllExamsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("END: GetExamList");
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetExamsPagingAsync([FromQuery] ExamSearch examSearch)
        {
            var query = new GetExamsPagingQuery(examSearch.CategoryId, examSearch.Name, examSearch.PageNumber,
                examSearch.PageSize);

            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamAsync([FromBody] CreateExamRequest request)
        {
            var command = new CreateExamCommand(request.Name, request.ShortDesc, request.Content,
                request.NumberOfQuestions, request.DurationInMinutes, request.Questions, request.Level,
                request.NumberOfQuestionCorrectForPass, request.IsTimeRestricted, request.AutoGenerateQuestion,
                request.CategoryId);

            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExamAsync([FromBody] UpdateExamRequest request)
        {
            var command = new UpdateExamCommand(request.Id, request.Name, request.ShortDesc, request.Content,
                request.NumberOfQuestion, request.DurationInMinutes, request.Questions, request.Level,
                request.NumberOfQuestionCorrectForPass, request.IsTimeRestricted, request.AutoGenerateQuestion,
                request.CategoryId);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamAsync(string id)
        {
            var command = new DeleteExamCommand(id);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamByIdAsync(string id)
        {
            var query = new GetExamByIdQuery(id);

            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }
    }
}
