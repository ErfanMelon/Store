using Store.Application.Services.Products.Queries.GetCategories;

namespace Store.Application.Services.Products.Queries.GetCategory
{
    public class CategoryDetailDto : CategoryDto
    {
        public List<ParentCategoryDto>? SubCategories { get; set; }
    }


}
