using Examination.Dtos.Enum;
using Examination.Shared.Questions;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Application.Commands.V1.Questions.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<QuestionDto>
    {
        [Required] public string Content { get; set; }

        [Required] public string CategoryId { get; }

        [Required] public QuestionType QuestionType { get; }

        [Required] public Level Level { get; }

        [Required] public List<AnswerDto> Answers { get; } = new List<AnswerDto>();

        public string Explain { get; set; }
    }
}
