using Examination.Domain.AggregateModels.ExamResultAggregate;
using Examination.Shared.SeedWork;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Exams.StartExam
{
    public class StartExamCommandHandler : IRequestHandler<StartExamCommand, ApiResult<bool>>
    {
        private readonly IExamResultRepository _repository;

        public StartExamCommandHandler(IExamResultRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<bool>> Handle(StartExamCommand request, CancellationToken cancellationToken)
        {
            var examResult = await _repository.GetDetails(request.UserId, request.ExamId);

            if (examResult != null)
            {
                examResult.ExamStartDate = DateTime.Now;
                examResult.Finished = false;
                examResult.StartExam(request.FirstName, request.LastName);
            }
            else
            {
                examResult = ExamResult.CreateNewResult(request.UserId, request.ExamId);
                examResult.StartExam(request.FirstName, request.LastName);
                await _repository.InsertAsync(examResult);
            }

            return new ApiSuccessResult<bool>((int)HttpStatusCode.OK, true);

        }
    }
}
