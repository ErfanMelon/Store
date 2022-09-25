namespace Store.EndPoint.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
