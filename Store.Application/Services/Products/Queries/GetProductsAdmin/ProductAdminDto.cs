namespace Store.Application.Services.Products.Queries.GetProductsAdmin
{
    public class ProductAdminDto
    {
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
    }
}
