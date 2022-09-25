namespace Store.Application.Services.Products.Queries.GetCategories
{
    public class CategoryDto
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public ParentCategoryDto Parent { get; set; }
        public bool HasChild { get; set; }
    }
}
