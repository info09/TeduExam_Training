using System.ComponentModel.DataAnnotations;

namespace Examination.Shared.Categories
{
    public class UpdateCategoryRequest
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string UrlPath { get; set; }
    }
}
