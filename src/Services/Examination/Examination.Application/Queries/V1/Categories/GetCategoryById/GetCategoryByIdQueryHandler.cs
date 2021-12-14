using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Application.Queries.V1.Exams.Categories.GetCategoryById;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Shared.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Examination.Application.Queries.V1.Categories.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClientSessionHandle _sessionHandle;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IClientSessionHandle sessionHandle, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _sessionHandle = sessionHandle ?? throw new ArgumentNullException(nameof(sessionHandle));
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetCategoryByIdQueryHandler");

            var result = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            var item = _mapper.Map<CategoryDto>(result);

            _logger.LogInformation("END: GetCategoryByIdQueryHandler");

            return item;
        }
    }
}
