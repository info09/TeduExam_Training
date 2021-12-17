using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Shared.SeedWork;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Exams.DeleteExam
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, ApiResult<bool>>
    {
        private readonly IExamRepository _examRepository;

        public DeleteExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<ApiResult<bool>> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var examToDelete = await _examRepository.GetExamByIdAsync(request.Id);

            if (examToDelete == null)
            {
                return new ApiErrorResult<bool>((int)HttpStatusCode.BadRequest,
                    $"Cannot find item to id = {request.Id}");
            }

            await _examRepository.DeleteAsync(request.Id);
            return new ApiSuccessResult<bool>((int)HttpStatusCode.OK, true, "Delete item success");
        }
    }
}