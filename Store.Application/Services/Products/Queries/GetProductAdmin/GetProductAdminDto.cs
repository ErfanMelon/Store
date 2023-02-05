namespace Store.Application.Services.Products.Queries.GetProductAdmin
{
    public class GetProductAdminDto
    {
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public List<string> CategoryTree { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public List<string> Images { get; set; }
        public List<(string,string)> Features { get; set; }
        public int TotalViews { get; set; }
        public int Stars { get; set; }
        public int OrderCount { get; set; }
    }
}
