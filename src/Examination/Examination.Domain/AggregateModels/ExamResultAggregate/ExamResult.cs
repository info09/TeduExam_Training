using Examination.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Examination.Domain.AggregateModels.ExamResultAggregate
{
    public class ExamResult : Entity, IAggregateRoot
    {
        public ExamResult(string userId, string examId)
        {
            UserId = userId;
            ExamId = examId;
            ExamStartDate = DateTime.Now;
            Finished = false;
        }

        [BsonElement("examId")]
        public string ExamId { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("examResultDetails")]
        public IEnumerable<ExamResultDetail> ExamResultDetails { get; set; }

        [BsonElement("examStartDate")]
        public DateTime ExamStartDate { get; set; }

        [BsonElement("examFinishDate")]
        public DateTime? ExamFinishDate { get; set; }

        [BsonElement("passed")]
        public bool? Passed { get; set; }

        [BsonElement("finished")]
        public bool Finished { get; set; }

        public static ExamResult CreateNewResult(string userId, string examId)
        {
            var result = new ExamResult(userId, examId);
            return result;
        }

        public void SetUserChoices(List<ExamResultDetail> examResultDetails)
        {
            ExamResultDetails = examResultDetails;
        }

        public void Finish()
        {
            Finished = true;
            ExamFinishDate = DateTime.Now;
        }
    }
}
