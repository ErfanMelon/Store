namespace Store.Application.Services.Products.Commands.AddCategory
{
    public class AddCategoryDto
    {
        public string CategoryTitle { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
