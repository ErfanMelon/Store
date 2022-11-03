using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductsAdmin
{
    public class GetProductsAdminDto
    {
        public List<ProductAdminDto> products { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
    }
}
