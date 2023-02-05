using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductAdmin;
public class GetProductAdminQuery : IRequest<ResultDto<GetProductAdminDto>>
{
    public GetProductAdminQuery(long productId)
    {
        ProductId = productId;
    }
    public GetProductAdminQuery()
    {

    }

    public long ProductId { get; set; }

    public class Handler : IRequestHandler<GetProductAdminQuery, ResultDto<GetProductAdminDto>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<GetProductAdminDto>> Handle(GetProductAdminQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (product is null)
                throw new ArgumentNullException("محصول معتبر نیست");

            GetProductAdminDto result = new GetProductAdminDto
            {
                ProductId = product.ProductId,
                Brand = product.Brand.Brand,
                Description = product.Description,
                Displayed = product.Displayed,
                Inventory = product.Inventory,
                Price = product.Price,
                ProductTitle = product.ProductTitle,
                TotalViews = product.Views,
                Images = product.ProductImages.Select(e => e.Src).ToList(),
                CategoryTree = GetCategoryTree(product.Category),
                OrderCount = CalculateOrders(product.ProductId),
                Features = GetFeatures(product.ProductFeatures),
                Stars = CalculateStars(product.Comments)
            };
            return new ResultDto<GetProductAdminDto>(result);
        }

        private List<(string, string)> GetFeatures(ICollection<ProductFeatures> productFeatures)
        {
            return productFeatures.Select(f => (f.Feature, f.FeatureValue)).ToList();
        }

        private int CalculateStars(ICollection<Comment> comments)
        {
            if (comments?.Any() ?? false)
                return (int)comments.Average(s => s.Score);
            return 0;
        }

        private int CalculateOrders(long productId)
        {
            var orders = _context.OrderDetails
                .Where(e => e.ProductId == productId && e.ProductState == OrderState.Delivered)
                .Select(e => e.Count)
                .ToList();
            if (orders.Any())
                return orders.Sum(e => e);
            return 0;
        }

        private List<string> GetCategoryTree(Category category)
        {
            List<string> result = new List<string>();

            if (category.ParentCategoryId.HasValue)
                result.AddRange(GetCategoryTree(category.ParentCategory));

            else
                result.Add(category.CategoryTitle);

            return result;
        }
    }
}
