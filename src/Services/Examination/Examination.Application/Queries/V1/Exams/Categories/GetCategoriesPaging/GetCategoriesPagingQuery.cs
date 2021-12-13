﻿using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Exams.Categories.GetCategoriesPaging
{
    public class GetCategoriesPagingQuery : IRequest<PagedList<CategoryDto>>
    {
        public string SearchKeyword { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
