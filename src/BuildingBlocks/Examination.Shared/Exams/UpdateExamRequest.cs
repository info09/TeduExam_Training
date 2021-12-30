﻿using Examination.Shared.Enum;
using Examination.Shared.Questions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Shared.Exams
{
    public class UpdateExamRequest
    {
        [Required] public string Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string ShortDesc { get; set; }

        public string Content { get; set; }

        [Required] public int NumberOfQuestions { get; set; }

        public int? DurationInMinutes { get; set; }

        public List<QuestionDto> Questions { get; set; }

        [Required] public Level Level { get; set; }

        [Required] public int NumberOfQuestionCorrectForPass { get; set; }

        [Required] public bool IsTimeRestricted { get; set; }

        public bool AutoGenerateQuestion { get; set; }

        [Required] public string CategoryId { get; set; }
    }
}
