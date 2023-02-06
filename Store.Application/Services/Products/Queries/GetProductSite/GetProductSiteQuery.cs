using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Products.Commands.VisitProduct;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductSite;
public class GetProductSiteDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public List<(string, string)> Features { get; set; }
    public int Stars { get; set; }
    public int TotalOrders { get; set; }
}
public class GetProductSiteQuery : IRequest<ResultDto<GetProductSiteDto>>
{
    public long ProductId { get; set; }
    public GetProductSiteQuery(long productId)
    {
        ProductId = productId;
    }
    public class Handler : IRequestHandler<GetProductSiteQuery, ResultDto<GetProductSiteDto>>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;
        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ResultDto<GetProductSiteDto>> Handle(GetProductSiteQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ThenInclude(i => i.ParentCategory)
                .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (product is null)
                throw new ArgumentNullException("محصول معتبر نیست");

            GetProductSiteDto result = new GetProductSiteDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductTitle,
                Brand = product.Brand.Brand,
                Category = GetCategory(product.Category),
                Description = product.Description,
                Price = product.Price,
                Features = GetFeatures(product.ProductFeatures),
                Images = product.ProductImages.Select(i => i.Src).ToList(),
                Stars = CalculateStars(product.ProductId),
                TotalOrders = CalculateOrders(product.ProductId)
            };

            await _mediator.Publish(new VisitProductCommand(product.ProductId));
            return new ResultDto<GetProductSiteDto>(result);
        }

        private int CalculateOrders(long productId)
        {
            var orders = _context.OrderDetails
                 .AsNoTracking()
                 .Where(e => e.ProductId == productId && e.ProductState == OrderState.Delivered)
                 .Select(e => e.Count);
            if (orders.Any())
                return orders.Sum(e => e);
            return 0;
        }

        private int CalculateStars(long productId)
        {
            var comments = _context.Comments
                .AsNoTracking()
                .Where(e => e.ProductId == productId);
            if (comments?.Any() ?? false)
                return (int)comments.Average(s => s.Score);
            return 0;
        }

        private List<(string, string)> GetFeatures(ICollection<ProductFeatures> productFeatures)
        {
            return productFeatures.Select(f => (f.Feature, f.FeatureValue)).ToList();
        }

        private string GetCategory(Category category)
        {
            if (category.ParentCategory != null)
            {
                return $"{GetCategory(category.ParentCategory)} - {category.CategoryTitle}";
            }
            return category.CategoryTitle;
        }
    }
}

