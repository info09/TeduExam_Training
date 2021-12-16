using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Examination.Application.Commands.V1.Categories.CreateCategory;
using Examination.Application.Commands.V1.Categories.DeleteCategory;
using Examination.Application.Commands.V1.Categories.UpdateCategory;
using Examination.Application.Queries.V1.Categories.GetAllCategories;
using Examination.Application.Queries.V1.Categories.GetCategoriesPaging;
using Examination.Application.Queries.V1.Categories.GetCategoryById;
using Examination.Shared.Categories;

namespace Examination.API.Controllers.V1
{
    public class CategoriesController : BasesController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            _logger.LogInformation("BEGIN: GetAllCategoriesAsync Controller");
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("END: GetAllCategoriesAsync Controller");

            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetCategoriesPagingAsync([FromQuery] CategorySearch categorySearch)
        {
            _logger.LogInformation("BEGIN: GetCategoriesAsync Controller");

            var query = new GetCategoriesPagingQuery(categorySearch.Name, categorySearch.PageSize, categorySearch.PageNumber);

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
