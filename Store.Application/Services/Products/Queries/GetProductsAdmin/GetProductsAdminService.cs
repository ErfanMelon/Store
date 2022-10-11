using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductsAdmin
{
    public class GetProductsAdminService : IGetProductsAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductsAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetProductsAdminDto> Execute(int page, int pagesize, string SearchKey)
        {

            int rowscount = 0;
            var Result = _context.Products
                .Include(p => p.Category)
                .Include(p=>p.Brand)
                .AsQueryable();
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Result = Result.Where(p =>
                p.Description.Contains(SearchKey) ||
                p.Brand.Brand.Contains(SearchKey) ||
                p.ProductTitle.Contains(SearchKey)||
                p.Category.CategoryTitle.Contains(SearchKey)
                ).AsQueryable();
            }
            var ProductList = Result.ToPaged(page, pagesize, out rowscount).
                 Select(p => new ProductAdminDto
                 {
                     Category = p.Category.CategoryTitle,
                     Price = p.Price,
                     ProductTitle = p.ProductTitle,
                     ProductId = p.ProductId,
                 }).ToList();
            return new ResultDto<GetProductsAdminDto>
            {
                Data = new GetProductsAdminDto
                {
                    products = ProductList,
                    CurrentPage = page,
                    PageSize = pagesize,
                    RowsCount = rowscount,
                },
                IsSuccess = true,
            };

        }
    }
}
