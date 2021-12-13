using Examination.Shared.Categories;
using MediatR;

namespace Examination.Application.Commands.V1.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }

        public string UrlPath { get; set; }
    }
}
