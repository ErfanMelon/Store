namespace Store.Application.Services.Products.Queries.GetCategories;

public class CategoryBriefDto
{
    public long CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public CategoryBriefDto? Parent { get; set; }
    public bool HasChild { get; set; } = false;
}

