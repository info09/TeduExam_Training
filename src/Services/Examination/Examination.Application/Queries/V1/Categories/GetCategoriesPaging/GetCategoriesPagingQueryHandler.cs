using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Examination.Application.Queries.V1.Categories.GetCategoriesPaging
{
    public class GetCategoriesPagingQueryHandler : IRequestHandler<GetCategoriesPagingQuery, PagedList<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClientSessionHandle _sessionHandle;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoriesPagingQueryHandler> _logger;

        public GetCategoriesPagingQueryHandler(ICategoryRepository categoryRepository, IClientSessionHandle sessionHandle, IMapper mapper, ILogger<GetCategoriesPagingQueryHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _sessionHandle = sessionHandle ?? throw new ArgumentNullException(nameof(sessionHandle));
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<CategoryDto>> Handle(GetCategoriesPagingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetCategoriesPagingQueryHandler");

            var result =
                await _categoryRepository.GetCategoriesPagingAsync(request.SearchKeyword, request.PageIndex,
                    request.PageSize);

            var items = _mapper.Map<List<CategoryDto>>(result.Item1);

            _logger.LogInformation("END: GetCategoriesPagingQueryHandler");

            return new PagedList<CategoryDto>(items, result.Item2, request.PageIndex, request.PageSize);
        }
    }
}
