using Examination.Application.Queries.V1.Exams.Categories.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Examination.Application.Commands.V1.Categories.CreateCategory;
using Examination.Application.Commands.V1.Categories.DeleteCategory;
using Examination.Application.Commands.V1.Categories.UpdateCategory;
using Examination.Application.Queries.V1.Categories.GetCategoriesPaging;
using Examination.Shared.Categories;

namespace Examination.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesPagingAsync([FromQuery] GetCategoriesPagingQuery query)
        {
            _logger.LogInformation("BEGIN: GetCategoriesAsync Controller");

            var result = await _mediator.Send(query);

            _logger.LogInformation("END: GetCategoriesAsync Controller");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(string id)
        {
            _logger.LogInformation("BEGIN: GetCategoryByIdAsync Controller");

            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query);

            _logger.LogInformation("END: GetCategoryByIdAsync Controller");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            _logger.LogInformation("BEGIN: CreateCategoryAsync Controller");

            var command = new CreateCategoryCommand()
            {
                Name = request.Name,
                UrlPath = request.UrlPath
            };
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest();

            _logger.LogInformation("END: CreateCategoryAsync Controller");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequest request)
        {
            _logger.LogInformation("BEGIN: UpdateCategoryAsync Controller");
            var command = new UpdateCategoryCommand(request.Id, request.Name, request.UrlPath);
            var result = await _mediator.Send(command);

            _logger.LogInformation("BEGIN: UpdateCategoryAsync Controller");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(string id)
        {
            _logger.LogInformation("BEGIN: DeleteCategoryAsync Controller");
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command);

            _logger.LogInformation("BEGIN: DeleteCategoryAsync Controller");
            return Ok(result);
        }
    }
}
