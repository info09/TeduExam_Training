using Examination.Shared.Categories;
using MediatR;

namespace Examination.Application.Commands.V1.Categories.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteCategoryCommand(string id)
        {
            Id = id;
        }
    }
}
