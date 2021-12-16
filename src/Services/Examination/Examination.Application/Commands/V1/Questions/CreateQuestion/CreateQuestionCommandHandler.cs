using AutoMapper;
using Examination.Application.Commands.V1.Categories.CreateCategory;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Enum;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Examination.Application.Extensions;

namespace Examination.Application.Commands.V1.Questions.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ApiResult<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResult<QuestionDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request.Answers?.Count(x => x.IsCorrect) > 1 && request.QuestionType == QuestionType.SingleSelection)
            {
                return new ApiErrorResult<QuestionDto>("Single choice question cannot have multiple correct answers.");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            var questionId = ObjectId.GenerateNewId().ToString();

            foreach (var item in request.Answers)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                }
            }

            var answers = _mapper.Map<List<AnswerDto>, List<Answer>>(request.Answers);

            var itemToAdd = new Question(questionId, request.Content, request.QuestionType, request.Level,
                request.CategoryId, answers, request.Explain, _httpContextAccessor.GetUserId(), category.Name);
            
            await _questionRepository.InsertAsync(itemToAdd);
            var result = _mapper.Map<Question, QuestionDto>(itemToAdd);
            return new ApiSuccessResult<QuestionDto>(result);

        }
    }
}
