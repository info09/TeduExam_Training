using AutoMapper;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.Categories.GetCategoriesPaging
{
    public class GetCategoriesPagingQueryHandler : IRequestHandler<GetCategoriesPagingQuery, ApiResult<PagedList<CategoryDto>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoriesPagingQueryHandler> _logger;

        public GetCategoriesPagingQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetCategoriesPagingQueryHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<PagedList<CategoryDto>>> Handle(GetCategoriesPagingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetCategoriesPagingQueryHandler");

            var result =
                await _categoryRepository.GetCategoriesPagingAsync(request.SearchKeyword, request.PageIndex,
                    request.PageSize);

            var items = _mapper.Map<List<CategoryDto>>(result.Items);

            _logger.LogInformation("END: GetCategoriesPagingQueryHandler");

            return new ApiSuccessResult<PagedList<CategoryDto>>(new PagedList<CategoryDto>(items,
                result.MetaData.TotalCount, request.PageIndex, request.PageSize));
        }
    }
}
