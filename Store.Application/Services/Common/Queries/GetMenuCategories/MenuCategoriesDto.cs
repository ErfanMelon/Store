namespace Store.Application.Services.Common.Queries.GetMenuCategories
{
    public class MenuCategoriesDto
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public bool HasChild { get; set; } = false;
        public List<MenuCategoriesDto>? Childs { get; set; }
    }
}
