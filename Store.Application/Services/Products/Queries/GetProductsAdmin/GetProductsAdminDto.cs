namespace Store.Application.Services.Products.Queries.GetProductsAdmin
{
    public class GetProductsAdminDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public List<ProductAdminDto> products { get; set; }
    }
}
