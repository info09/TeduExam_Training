using Examination.Dtos.Enum;
using System;
using System.Collections.Generic;

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
