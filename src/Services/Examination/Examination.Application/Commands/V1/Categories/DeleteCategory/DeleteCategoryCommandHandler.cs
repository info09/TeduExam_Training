using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Categories.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResult<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<DeleteCategoryCommand> _logger;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<DeleteCategoryCommand> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<ApiResult<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var itemToDelete = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            if (itemToDelete == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return new ApiErrorResult<bool>((int)HttpStatusCode.BadRequest, $"Item is not found {request.Id}");
            }

            await _categoryRepository.DeleteAsync(request.Id);
            return new ApiSuccessResult<bool>((int)HttpStatusCode.OK, true, "Delete successful");

        }
    }
}
