using AutoMapper;
using Examination.Domain.AggregateModels.QuestionAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Questions.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteQuestionCommandHandler> _logger;

        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper, ILogger<DeleteQuestionCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToDelete = await _questionRepository.GetQuestionByIdAsync(request.Id);
            if (itemToDelete == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return false;
            }
            try
            {
                await _questionRepository.DeleteAsync(request.Id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
