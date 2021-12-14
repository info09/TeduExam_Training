using Examination.Shared.Questions;
using MediatR;

namespace Examination.Application.Queries.V1.Questions.GetQuestionById
{
    public class GetQuestionByIdQuery : IRequest<QuestionDto>
    {
        public string Id { get; set; }

        public GetQuestionByIdQuery(string id)
        {
            Id = id;
        }
    }
}
