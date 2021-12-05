using Examination.Application.Queries.GetHomeExamList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamListAsync()
        {
            var query = new GetHomeExamListQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
