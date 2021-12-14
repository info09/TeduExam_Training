using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Infrastructure.MongoDb.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Tuple<List<Question>, long>> GetQuestionsPagingAsync(string searchKeyword, int pageIndex, int pageSize)
        {
            FilterDefinition<Question> filter = Builders<Question>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                filter = Builders<Question>.Filter.Eq(i => i.Content, searchKeyword);
            }

            var totalRow = await Collection.Find(filter).CountDocumentsAsync();
            var items = await Collection.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            return new Tuple<List<Question>, long>(items, totalRow);
        }

        public async Task<Question> GetQuestionByIdAsync(string id)
        {
            FilterDefinition<Question> filter = Builders<Question>.Filter.Eq(i => i.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Question> GetQuestionByContentAsync(string content)
        {
            FilterDefinition<Question> filter = Builders<Question>.Filter.Eq(i => i.Content, content);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
