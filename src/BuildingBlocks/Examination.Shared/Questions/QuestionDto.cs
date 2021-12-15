using System;
using System.Collections.Generic;
using Examination.Shared.Enum;

namespace Examination.Shared.Questions
{
    public class QuestionDto
    {
        public string Content { get; set; }

        public QuestionType QuestionType { get; set; }

        public Level Level { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }

        public string Explain { get; set; }

        public DateTime DateCreated { get; set; }

        public string OwnerUserId { get; set; }
    }
}
