using Examination.Shared.Questions;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examination.Shared.Enum;

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

        public CreateQuestionCommand(string content, string categoryId, QuestionType questionType, Level level, string explain)
        {
            Content = content;
            CategoryId = categoryId;
            QuestionType = questionType;
            Level = level;
            Explain = explain;
        }
    }
}
