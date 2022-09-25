using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(EditCategoryDto request);
    }
    public class EditCategoryDto
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
