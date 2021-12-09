using Examination.Application.Queries.V1.GetHomeExamList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Examination.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExamsController : ControllerBase
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
