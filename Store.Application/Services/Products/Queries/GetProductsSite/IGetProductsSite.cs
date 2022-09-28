using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductsSite
{
    public interface IGetProductsSite
    {
        ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey, int PageSize, OrderProduct order);
    }
    public enum OrderProduct
    {
        /// <summary>
        /// دسته بندی نشده
        /// </summary>
        NotOrdered,
        /// <summary>
        /// بیشترین بازدید
        /// </summary>
        MostVisited,
        /// <summary>
        /// بیشترین فروش
        /// </summary>
        MostSales,
        /// <summary>
        /// محبوب
        /// </summary>
        Popular,
        /// <summary>
        /// جدیدترین ها
        /// </summary>
        Newsest,
        /// <summary>
        /// ارزانترین ها
        /// </summary>
        Cheaper,
        /// <summary>
        /// گرانترین ها
        /// </summary>
        Expensive
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

        public ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey, int PageSize, OrderProduct order)
        {
            try
            {
                var products = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductLikes)
                    .Include(p => p.Brand)
                    .AsQueryable();

                if (CategoryId != null)
                {
                    products = products.Where(p => p.Category.CategoryId == CategoryId || p.Category.ParentCategoryId == CategoryId).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(SearchKey))
                {
                    products = products.Where(p => p.ProductTitle.Contains(SearchKey) || p.Brand.Brand.Contains(SearchKey)).AsQueryable();
                }
                products = products.ToPaged(page, PageSize, out int rowcount).AsQueryable();
                switch (order)
                {
                    case OrderProduct.NotOrdered:
                        break;
                    case OrderProduct.MostVisited:
                        products = products.OrderByDescending(p => p.Views).AsQueryable();
                        break;
                    case OrderProduct.MostSales:// Incomplete !
                        break;
                    case OrderProduct.Popular:
                        products = products.OrderByDescending(p => p).AsQueryable();
                        break;
                    case OrderProduct.Newsest:
                        products = products.OrderByDescending(p => p.InsertTime).AsQueryable();
                        break;
                    case OrderProduct.Cheaper:
                        products = products.OrderBy(p => p.Price).AsQueryable();
                        break;
                    case OrderProduct.Expensive:
                        products = products.OrderByDescending(p => p.Price).AsQueryable();
                        break;
                    default:
                        break;
                }
                return new ResultDto<ResultGetProductsSiteDto>
                {
                    Data = new ResultGetProductsSiteDto
                    {
                        Products = products.ToList().Select(p => new ProductsSiteDto
                        {
                            Price = p.Price,
                            ProductId = p.ProductId,
                            ProductTitle = p.ProductTitle,
                            Stars = p.ProductLikes.Any() ? (int)p.ProductLikes.Select(l => l.Score).Average() : 0,
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
