using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductsSite
{
    public interface IGetProductsSite
    {
        ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey);
    }
    public class ProductsSiteDto
    {
        public long ProductId { get; set; }
        public int Stars { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public string ImageSrc { get; set; }
    }
    public class ResultGetProductsSiteDto
    {
        public int RowSize { get; set; }
        public List<ProductsSiteDto> Products { get; set; }
    }
    public class GetProductsSite : IGetProductsSite
    {
        private readonly IDataBaseContext _context;
        public GetProductsSite(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey)
        {
            try
            {
                Random random = new Random();
                var products = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .AsQueryable();

                if (CategoryId != null)
                {
                    products = products.Where(p => p.Category.CategoryId == CategoryId || p.Category.ParentCategoryId == CategoryId).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(SearchKey))
                {
                    products = products.Where(p => p.ProductTitle.ToLower().Contains(SearchKey) || p.Brand.ToLower().Contains(SearchKey)).AsQueryable();
                }
                products.ToPaged(page, 10, out int rowcount);
                return new ResultDto<ResultGetProductsSiteDto>
                {
                    Data = new ResultGetProductsSiteDto
                    {
                        Products = products.ToList().Select(p => new ProductsSiteDto
                        {
                            Price = p.Price,
                            ProductId = p.ProductId,
                            ProductTitle = p.ProductTitle,
                            Stars = random.Next(1, 5),
                            ImageSrc = p.ProductImages.Select(i => i.Src).FirstOrDefault()
                        }).ToList(),
                        RowSize = rowcount,
                    },
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultGetProductsSiteDto> { Message = "خطا !" };
            }
        }
    }
}
