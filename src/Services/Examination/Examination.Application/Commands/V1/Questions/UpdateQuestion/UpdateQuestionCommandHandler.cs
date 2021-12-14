using AutoMapper;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Questions.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, bool>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuestionCommandHandler> _logger;

        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper, ILogger<UpdateQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _questionRepository.GetQuestionByIdAsync(request.Id);
            if (itemToUpdate == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return false;
            }

            itemToUpdate.Content = request.Content;
            itemToUpdate.QuestionType = request.QuestionType;
            itemToUpdate.Level = request.Level;
            itemToUpdate.CategoryId = request.CategoryId;
            itemToUpdate.QuestionType = request.QuestionType;
            var answers = _mapper.Map<List<AnswerDto>, List<Answer>>(request.Answers);
            itemToUpdate.Answers = answers;
            itemToUpdate.Explain = request.Explain;
            try
            {
                await _questionRepository.UpdateAsync(itemToUpdate);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
