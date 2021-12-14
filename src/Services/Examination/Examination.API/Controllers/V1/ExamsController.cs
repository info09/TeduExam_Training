using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Examination.API.Controllers.V1;
using Examination.Application.Queries.V1.Exams.GetHomeExamList;

namespace Examination.API.Controllers
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
            var query = new GetHomeExamListQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("END: GetExamList");
            return Ok(result);
        }
    }
}
