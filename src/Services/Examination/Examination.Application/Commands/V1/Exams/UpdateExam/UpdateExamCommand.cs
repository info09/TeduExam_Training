using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examination.Shared.Enum;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Commands.V1.Exams.UpdateExam
{
    public class UpdateExamCommand : IRequest<ApiResult<bool>>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortDesc { get; set; }

        public string Content { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; }

        public int? DurationInMinutes { get; set; }

        public List<QuestionDto> Questions { get; set; }

        [Required]
        public Level Level { get; set; }

        [Required]
        public int NumberOfQuestionCorrectForPass { get; set; }

        [Required]
        public bool IsTimeRestricted { get; set; }

        public bool AutoGenerateQuestion { set; get; }

        [Required]
        public string CategoryId { get; set; }

        public UpdateExamCommand(string id, string name, string shortDesc, string content, int numberOfQuestions, int? durationInMinutes, List<QuestionDto> questions, Level level, int numberOfQuestionCorrectForPass, bool isTimeRestricted, bool autoGenerateQuestion, string categoryId)
        {
            Id = id;
            Name = name;
            ShortDesc = shortDesc;
            Content = content;
            NumberOfQuestions = numberOfQuestions;
            DurationInMinutes = durationInMinutes;
            Questions = questions;
            Level = level;
            NumberOfQuestionCorrectForPass = numberOfQuestionCorrectForPass;
            IsTimeRestricted = isTimeRestricted;
            AutoGenerateQuestion = autoGenerateQuestion;
            CategoryId = categoryId;
        }
    }
}