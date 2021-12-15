using AutoMapper;
using Examination.Application.Commands.V1.Categories.CreateCategory;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Questions.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ApiResult<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResult<QuestionDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToAdd = await _questionRepository.GetQuestionByContentAsync(request.Content);

            if (itemToAdd != null)
            {
                _logger.LogError($"Item name exist: {request.Content}");
                return null;
            }

            var questionId = ObjectId.GenerateNewId().ToString();
            var answers = _mapper.Map<List<AnswerDto>, List<Answer>>(request.Answers);
            itemToAdd = new Question(id: questionId, content: request.Content, questionType: request.QuestionType,
                level: request.Level, categoryId: request.CategoryId, answers: answers, explain: request.Explain,
                ownerUserId: null);


            await _questionRepository.InsertAsync(itemToAdd);
            var result = _mapper.Map<Question, QuestionDto>(itemToAdd);
            return new ApiSuccessResult<QuestionDto>(result);

        }
    }
}
