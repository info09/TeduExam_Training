﻿using Examination.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examination.Infrastructure.MongoDb.SeedWork;

namespace Examination.Infrastructure.MongoDb.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            IMongoClient mongoClient,
            IOptions<ExamSettings> settings,
            IMediator mediator)
            : base(mongoClient, settings, Constants.Collections.Category)
        {
        }

        public async Task<Tuple<List<Category>, long>> GetCategoriesPagingAsync(string searchKeyword, int pageIndex, int pageSize)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                filter = Builders<Category>.Filter.Eq(i => i.Name, searchKeyword);
            }

            var totalRow = await Collection.Find(filter).CountDocumentsAsync();
            var items = await Collection.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            return new Tuple<List<Category>, long>(items, totalRow);
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(i => i.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(i => i.Name, name);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
