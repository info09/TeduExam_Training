using AutoMapper;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.Questions.GetQuestionById
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, ApiResult<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuestionByIdQueryHandler> _logger;

        public GetQuestionByIdQueryHandler(IQuestionRepository questionRepository, IMapper mapper, ILogger<GetQuestionByIdQueryHandler> logger)
        {
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("BEGIN: GetQuestionByIdAsync");
            var result = await _questionRepository.GetQuestionByIdAsync(request.Id);
            var data = _mapper.Map<QuestionDto>(result);
            _logger.LogInformation("END: GetQuestionByIdAsync");
            return new ApiSuccessResult<QuestionDto>(data);
        }
    }
}
