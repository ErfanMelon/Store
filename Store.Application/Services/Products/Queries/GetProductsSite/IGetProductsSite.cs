using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductsSite
{
    public interface IGetProductsSite
    {
        ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey, int pagesize, Order order);
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
        public List<ProductsSiteDto> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
    }
    public class GetProductsSite : IGetProductsSite
    {
        private readonly IDataBaseContext _context;
        public GetProductsSite(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetProductsSiteDto> Execute(int page, long? CategoryId, string SearchKey, int pagesize, Order order)
        {

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
                products = products.Where(p => p.ProductTitle.ToLower().Contains(SearchKey)).AsQueryable();// Brand Adds In Future
            }
            switch (order)
            {
                case Order.NotOrdered:
                    break;
                case Order.MostVisited:
                    products = products.OrderByDescending(p => p.Views).AsQueryable();
                    break;
                case Order.Bestselling:
                    products = products.OrderByDescending(
                        p => _context.OrderDetails.Any(d => d.ProductId == p.ProductId) ?
                       _context.OrderDetails.Where(d => d.ProductId == p.ProductId)
                        .Sum(s => s.Count) : 0).AsQueryable();
                    break;
                case Order.MostPopular:
                    products = products.OrderByDescending(p => p).AsQueryable();
                    break;
                case Order.Newest:
                    products = products.OrderByDescending(p => p.InsertTime).AsQueryable();
                    break;
                case Order.MostExpensive:
                    products = products.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                case Order.Cheapest:
                    products = products.OrderBy(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }
            products = products.Where(p => p.Displayed).ToPaged(page, pagesize, out int rowcount).AsQueryable();
            return new ResultDto<ResultGetProductsSiteDto>
            {
                Data = new ResultGetProductsSiteDto
                {
                    Products = products.ToList().Select(p => new ProductsSiteDto
                    {
                        Price = p.Price,
                        ProductId = p.ProductId,
                        ProductTitle = p.ProductTitle,
                        Stars = _context.Comments.Any(c => c.ProductId == p.ProductId) ? (int)_context.Comments.Average(l => l.Score) : 0,
                        ImageSrc = p.ProductImages.Select(i => i.Src).FirstOrDefault()
                    }).ToList(),
                    RowsCount = rowcount,
                    CurrentPage = page,
                    PageSize = pagesize
                },
                IsSuccess = true
            };

        }
    }
    /// <summary>
    /// وضعیت مرتب سازی
    /// </summary>
    public enum Order
    {
        /// <summary>
        /// مرتب نشده
        /// </summary>
        NotOrdered,
        /// <summary>
        /// بیشترین بازدید
        /// </summary>
        MostVisited,
        /// <summary>
        /// پرفروش
        /// </summary>
        Bestselling,
        /// <summary>
        /// محبوب
        /// </summary>
        MostPopular,
        /// <summary>
        /// جدیدترین
        /// </summary>
        Newest,
        /// <summary>
        /// ارزانترین
        /// </summary>
        Cheapest,
        /// <summary>
        /// گرانترین
        /// </summary>
        MostExpensive
    }
}
