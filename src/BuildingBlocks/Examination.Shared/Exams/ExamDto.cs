using System;
using Examination.Shared.Enum;

namespace Examination.Shared.Exams
{
    public class ExamDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public int NumberOfQuestion { get; set; }

        public TimeSpan? Duration { get; set; }

        public Level Level { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
