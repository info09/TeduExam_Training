using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Infrastructure.MongoDb.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Examination.Infrastructure.MongoDb.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(
            IMongoClient mongoClient,
            IOptions<ExamSettings> settings,
            IMediator mediator)
            : base(mongoClient, settings, Constants.Collections.Question)
        {
        }
    }
}
