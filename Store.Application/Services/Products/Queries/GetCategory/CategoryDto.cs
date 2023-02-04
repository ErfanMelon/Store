namespace Store.Application.Services.Products.Queries.GetCategory;

/// <summary>
/// This Dto is for show the detail of a category
/// </summary>
public class CategoryDto
{
    public long CategoryId { get; }
    public string CategoryTitle { get; }
    public CategoryDto? Parent { get; }
    public List<CategoryDto>? Children { get; }

    public CategoryDto(long categoryId, string categoryTitle, CategoryDto? parent=null, List<CategoryDto>? children=null)
    {
        CategoryId = categoryId;
        CategoryTitle = categoryTitle;
        Parent = parent;
        Children = children;
    }
}



