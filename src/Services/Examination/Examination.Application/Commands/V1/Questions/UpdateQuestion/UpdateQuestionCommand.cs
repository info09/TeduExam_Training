using Examination.Shared.Questions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examination.Shared.Enum;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Commands.V1.Questions.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<ApiResult<bool>>
    {
        [Required] public string Id { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; }

        public QuestionType QuestionType { get; }

        public Level Level { get; }

        public List<AnswerDto> Answers { get; }

        public string Explain { get; set; }

        public UpdateQuestionCommand(string id, string content, string categoryId, QuestionType questionType, Level level, List<AnswerDto> answers, string explain)
        {
            Id = id;
            Content = content;
            CategoryId = categoryId;
            QuestionType = questionType;
            Level = level;
            Answers = answers;
            Explain = explain;
        }
    }
}
