using AutoMapper;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Questions.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ApiResult<bool>>
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

        public async Task<ApiResult<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToDelete = await _questionRepository.GetQuestionByIdAsync(request.Id);
            if (itemToDelete == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return new ApiErrorResult<bool>($"Item is not found {request.Id}");
            }

            await _questionRepository.DeleteAsync(request.Id);
            return new ApiSuccessResult<bool>(true, "Delete successful");

        }
    }
}
