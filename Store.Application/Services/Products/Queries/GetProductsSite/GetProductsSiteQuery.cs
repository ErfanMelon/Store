using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductsSite;
public class GetProductsSiteQuery : IRequest<ResultDto<PaginationDto<ProductsSiteDto>>>
{
    public GetProductsSiteQuery(int page, int pageSize, long? categoryId, string searchKey = null, Order arrange = Order.NotOrdered)
    {
        Page = page;
        PageSize = pageSize;
        CategoryId = categoryId;
        SearchKey = searchKey;
        Arrange = arrange;
    }

    public int Page { get; }
    public int PageSize { get; }
    public long? CategoryId { get; }
    public string? SearchKey { get; }
    public Order Arrange { get; }

    public class Handler : IRequestHandler<GetProductsSiteQuery, ResultDto<PaginationDto<ProductsSiteDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public Task<ResultDto<PaginationDto<ProductsSiteDto>>> Handle(GetProductsSiteQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.Comments)
                .AsQueryable();

            if (request.CategoryId.HasValue)
                query = query
                    .Where(p => p.Category.CategoryId == request.CategoryId ||
                    p.Category.ParentCategoryId == request.CategoryId)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchKey))
                query = query
                    .Where(p => p.ProductTitle.ToLower().Contains(request.SearchKey) ||
                    p.Brand.Brand.ToLower().Contains(request.SearchKey))
                    .AsQueryable();

            switch (request.Arrange)
            {
                case Order.NotOrdered:
                    break;
                case Order.MostVisited:
                    query = query.OrderByDescending(p => p.Views).AsQueryable();
                    break;
                case Order.Bestselling:
                    query = query.OrderByDescending(
                        p => _context.OrderDetails.Any(d => d.ProductId == p.ProductId) ?
                       _context.OrderDetails.Where(d => d.ProductId == p.ProductId)
                        .Sum(s => s.Count) : 0).AsQueryable();
                    break;
                case Order.MostPopular:
                    query = query.OrderByDescending(p => p).AsQueryable();
                    break;
                case Order.Newest:
                    query = query.OrderByDescending(p => p.InsertTime).AsQueryable();
                    break;
                case Order.MostExpensive:
                    query = query.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                case Order.Cheapest:
                    query = query.OrderBy(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }
            var result = query
                .Where(p => p.Displayed)
                .Select(p => new ProductsSiteDto
                {
                    ImageSrc = p.ProductImages.Any() ? p.ProductImages.First().Src : "",
                    Price = p.Price,
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductTitle,
                    Stars = CalculateStars(p.Comments)
                })
                .ToPaged(request.Page, request.PageSize);
            return Task.FromResult(new ResultDto<PaginationDto<ProductsSiteDto>>(result));
        }

        private static int CalculateStars(ICollection<Comment> comments)
        {
            if (comments?.Any() ?? false)
                return (int)comments.Average(s => s.Score);
            return 0;
        }

    }
}
public class ProductsSiteDto
{
    public long ProductId { get; set; }
    public int Stars { get; set; }
    public string ProductTitle { get; set; }
    public int Price { get; set; }
    public string ImageSrc { get; set; }
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
