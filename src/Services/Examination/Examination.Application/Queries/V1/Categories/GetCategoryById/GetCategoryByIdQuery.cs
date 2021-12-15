using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Queries.V1.Categories.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<ApiResult<CategoryDto>>
    {
        public string Id { get; set; }

        public GetCategoryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
