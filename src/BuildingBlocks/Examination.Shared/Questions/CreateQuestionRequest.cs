using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examination.Shared.Enum;

namespace Examination.Shared.Questions
{
    public class CreateQuestionRequest
    {
        [Required] public string Content { get; set; }

        [Required] public QuestionType QuestionType { get; set; }

        [Required] public Level Level { get; set; }

        [Required] public string CategoryId { get; set; }

        [Required] public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();

        public string Explain { get; set; }
    }
}
