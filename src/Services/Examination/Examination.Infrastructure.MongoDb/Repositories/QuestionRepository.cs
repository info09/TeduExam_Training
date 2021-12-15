using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Infrastructure.MongoDb.SeedWork;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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

        public async Task<PagedList<Question>> GetQuestionsPagingAsync(string categoryId, string searchKeyword, int pageIndex, int pageSize)
        {
            FilterDefinition<Question> filter = Builders<Question>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                filter = Builders<Question>.Filter.Where(i => i.Content.ToLower().Contains(searchKeyword.ToLower()));
            }

            if (!string.IsNullOrEmpty(categoryId))
                filter = Builders<Question>.Filter.Eq(i => i.CategoryId, categoryId);

            var totalRow = await Collection.Find(filter).CountDocumentsAsync();
            var items = await Collection.Find(filter).SortByDescending(i => i.DateCreated).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            return new PagedList<Question>(items, totalRow, pageIndex, pageSize);
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
