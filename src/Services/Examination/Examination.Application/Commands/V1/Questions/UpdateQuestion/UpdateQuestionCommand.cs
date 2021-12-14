using Examination.Dtos.Enum;
using Examination.Shared.Questions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Examination.Application.Commands.V1.Questions.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<bool>
    {
        [Required] public string Id { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; }

        public QuestionType QuestionType { get; }

        public Level Level { get; }

        public List<AnswerDto> Answers { get; } = new List<AnswerDto>();

        public string Explain { get; set; }

        public UpdateQuestionCommand(string id, string content, string categoryId, QuestionType questionType, Level level, string explain)
        {
            Id = id;
            Content = content;
            CategoryId = categoryId;
            QuestionType = questionType;
            Level = level;
            Explain = explain;
        }
    }
}
