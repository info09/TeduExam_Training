using System.Threading.Tasks;
using Examination.Application.Commands.V1.Questions.CreateQuestion;
using Examination.Application.Commands.V1.Questions.DeleteQuestion;
using Examination.Application.Commands.V1.Questions.UpdateQuestion;
using Examination.Application.Queries.V1.Questions.GetQuestionById;
using Examination.Application.Queries.V1.Questions.GetQuestionsPaging;
using Examination.Shared.Questions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Examination.API.Controllers.V1
{
    public class QuestionsController : BasesController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IMediator mediator, ILogger<QuestionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionsPagingAsync([FromQuery] QuestionSearch questionSearch)
        {
            _logger.LogInformation("BEGIN: GetQuestionsPagingAsync");
            var query = new GetQuestionsPagingQuery(questionSearch.Name, questionSearch.PageSize,
                questionSearch.PageNumber);
            var result = await _mediator.Send(query);
            _logger.LogInformation("END: GetQuestionsPagingAsync");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionByIdAysnc(string id)
        {
            _logger.LogInformation("BEGIN: GetQuestionByIdAysnc");
            var query = new GetQuestionByIdQuery(id);
            var result = await _mediator.Send(query);
            _logger.LogInformation("END: GetQuestionByIdAysnc");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionRequest request)
        {
            _logger.LogInformation("BEGIN: CreateQuestionAsync");
            var command = new CreateQuestionCommand(request.Content, request.CategoryId, request.QuestionType,
                request.Level, request.Explain);

            var result = await _mediator.Send(command);
            _logger.LogInformation("END: CreateQuestionAsync");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionRequest request)
        {
            _logger.LogInformation("BEGIN: UpdateQuestionAsync");
            var command = new UpdateQuestionCommand(request.Id, request.Content, request.CategoryId,
                request.QuestionType, request.Level, request.Explain);

            var result = await _mediator.Send(command);
            _logger.LogInformation("END: UpdateQuestionAsync");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(string id)
        {
            _logger.LogInformation("BEGIN: DeleteQuestionAsync");
            var command = new DeleteQuestionCommand(id);

            var result = await _mediator.Send(command);
            _logger.LogInformation("END: DeleteQuestionAsync");
            return Ok(result);
        }
    }
}
