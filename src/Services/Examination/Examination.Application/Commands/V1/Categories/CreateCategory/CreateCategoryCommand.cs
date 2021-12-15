using Examination.Shared.Categories;
using Examination.Shared.SeedWork;
using MediatR;

namespace Examination.Application.Commands.V1.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResult<CategoryDto>>
    {
        public string Name { get; set; }

        public string UrlPath { get; set; }
    }
}
