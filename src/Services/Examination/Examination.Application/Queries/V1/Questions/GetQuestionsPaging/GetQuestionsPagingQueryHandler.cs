using AutoMapper;
using Examination.Application.Queries.V1.Questions.GetQuestionById;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.Questions.GetQuestionsPaging
{
    public class GetQuestionsPagingQueryHandler : IRequestHandler<GetQuestionsPagingQuery, ApiResult<PagedList<QuestionDto>>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;

        public GetQuestionsPagingQueryHandler(IQuestionRepository questionRepository, IMapper mapper, ILogger<GetQuestionByIdQueryHandler> logger)
        {
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<PagedList<QuestionDto>>> Handle(GetQuestionsPagingQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetQuestionsPagingQueryHandler");

            var result =
                await _questionRepository.GetQuestionsPagingAsync(request.CategoryId, request.SearchKeyword, request.PageIndex,
                    request.PageSize);

            var items = _mapper.Map<List<QuestionDto>>(result.Items);

            _logger.LogInformation("END: GetQuestionsPagingQueryHandler");

            return new ApiSuccessResult<PagedList<QuestionDto>>(new PagedList<QuestionDto>(items,
                result.MetaData.TotalCount, request.PageIndex, request.PageSize));
        }
    }
}
