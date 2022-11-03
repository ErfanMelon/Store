using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductSite
{
    public interface IGetProductSiteService
    {
        ResultDto<GetProductSiteDto> Execute(long productId);
    }
    public class GetProductSiteService : IGetProductSiteService
    {
        private readonly IDataBaseContext _context;
        public GetProductSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetProductSiteDto> Execute(long productId)
        {

            var product = _context.Products
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ThenInclude(i => i.ParentCategory)
                .FirstOrDefault(p => p.ProductId == productId);
            product.Views++;
            _context.SaveChanges();
            return new ResultDto<GetProductSiteDto>
            {
                Data = new GetProductSiteDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductTitle,
                    Brand = product.Brand.Brand,
                    Category = GetCategory(product.Category),
                    Description = product.Description,
                    Price = product.Price,
                    Features = product.ProductFeatures.Select(f => new ProductSiteFeaturesDto { Feature = f.Feature, FeatureValue = f.FeatureValue }).ToList(),
                    Images = product.ProductImages.Select(i => i.Src).ToList(),
                    Stars=_context.ProductLikes.Any(s=>s.ProductId== productId)? (int)_context.ProductLikes.Where(s => s.ProductId == productId).Average(e=>e.Score):0, // average the userlikes
                    TotalOrders=_context.OrderDetails.Any(o => o.ProductId == productId)?_context.OrderDetails.Where(o => o.ProductId == productId).Sum(o=>o.Count):0 // sum total order of this product
                },
                IsSuccess = true,
            };

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
    public class GetProductSiteDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public List<ProductSiteFeaturesDto> Features { get; set; }
        public int Stars { get; set; }
        public int TotalOrders { get; set; }
    }
    public class ProductSiteFeaturesDto
    {
        public string Feature { get; set; }
        public string FeatureValue { get; set; }
    }
}
