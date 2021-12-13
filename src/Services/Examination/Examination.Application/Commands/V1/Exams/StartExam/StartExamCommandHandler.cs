using System;
using System.Threading;
using System.Threading.Tasks;
using Examination.Domain.AggregateModels.ExamResultAggregate;
using MediatR;

namespace Examination.Application.Commands.V1.Exams.StartExam
{
    public class StartExamCommandHandler : IRequestHandler<StartExamCommand, bool>
    {
        private readonly IExamResultRepository _repository;

        public StartExamCommandHandler(IExamResultRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(StartExamCommand request, CancellationToken cancellationToken)
        {
            var examResult = await _repository.GetDetails(request.UserId, request.ExamId);
            _repository.StartTransaction();

            try
            {
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

                await _repository.CommitTransactionAsync(examResult, cancellationToken);
                return true;
            }
            catch
            {
                await _repository.AbortTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
