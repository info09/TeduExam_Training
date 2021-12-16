using System;
using AutoMapper;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Examination.Application.Extensions;
using Examination.Domain.AggregateModels.CategoryAggregate;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Examination.Application.Commands.V1.Questions.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, ApiResult<bool>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuestionCommandHandler> _logger;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<UpdateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<bool>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _questionRepository.GetQuestionByIdAsync(request.Id);
            if (itemToUpdate == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return new ApiErrorResult<bool>($"Item is not found {request.Id}");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);

            foreach (var item in request.Answers)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                }
            }

            itemToUpdate.Content = request.Content;
            itemToUpdate.QuestionType = request.QuestionType;
            itemToUpdate.Level = request.Level;
            itemToUpdate.CategoryId = request.CategoryId;
            itemToUpdate.QuestionType = request.QuestionType;
            var answers = _mapper.Map<List<AnswerDto>, List<Answer>>(request.Answers);
            itemToUpdate.Answers = answers;
            itemToUpdate.Explain = request.Explain;
            itemToUpdate.CategoryName = category.Name;
            itemToUpdate.OwnerUserId = _httpContextAccessor.GetUserId();

            await _questionRepository.UpdateAsync(itemToUpdate);
            return new ApiSuccessResult<bool>(true, "Update success");

        }
    }
}
