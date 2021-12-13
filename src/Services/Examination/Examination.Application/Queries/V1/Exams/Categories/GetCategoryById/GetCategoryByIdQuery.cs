using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examination.Shared.Categories;
using MediatR;

namespace Examination.Application.Queries.V1.Exams.Categories.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>, IRequest<Unit>
    {
        public string Id { get; set; }

        public GetCategoryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
