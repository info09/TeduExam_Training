using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Commands.V1.Exams.DeleteExam
{
    public class DeleleExamCommand : IRequest<ApiResult<bool>>
    {
        public string Id { get; set; }

        public DeleleExamCommand(string id)
        {
            Id = id;
        }
    }
}