using Examination.Domain.AggregateModels.ExamAggregate;
using Examination.Infrastructure.MongoDb.SeedWork;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination.Infrastructure.MongoDb.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(
            IMongoClient mongoClient,
            IOptions<ExamSettings> settings,
            IMediator mediator)
            : base(mongoClient, settings, Constants.Collections.Exam)
        {
        }

        public async Task<Exam> GetExamByIdAsync(string id)
        {
            var filter = Builders<Exam>.Filter.Eq(s => s.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Exam>> GetExamsPagingAsync(string categoryId, string searchKeyword, int pageIndex, int pageSize)
        {
            var filter = Builders<Exam>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchKeyword))
                filter = Builders<Exam>.Filter.Where(i => i.Name.ToLower().Contains(searchKeyword.ToLower()));

            if (!string.IsNullOrEmpty(categoryId))
                filter = Builders<Exam>.Filter.Eq(i => i.CategoryId, categoryId);

            var totalRow = await Collection.Find(filter).CountDocumentsAsync();
            var items = await Collection.Find(filter)
                .SortByDescending(i => i.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PagedList<Exam>(items, totalRow, pageIndex, pageSize);
        }

        public async Task<IEnumerable<Exam>> GetAllExamListAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }
    }
}
