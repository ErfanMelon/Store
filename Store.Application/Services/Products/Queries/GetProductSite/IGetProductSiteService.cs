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
            try
            {
                var product = _context.Products
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Category)
                    .ThenInclude(i => i.ParentCategory)
                    .FirstOrDefault();
                return new ResultDto<GetProductSiteDto>
                {
                    Data = new GetProductSiteDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductTitle,
                        Brand = product.Brand,
                        Category = GetCategory(product.Category),
                        Description = product.Description,
                        Price = product.Price,
                        Features = product.ProductFeatures.Select(f => new ProductSiteFeaturesDto { Feature = f.Feature, FeatureValue = f.FeatureValue }).ToList(),
                        Images=product.ProductImages.Select(i=>i.Src).ToList()
                    },
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                return new ResultDto<GetProductSiteDto> { };
            }
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
    }
    public class ProductSiteFeaturesDto
    {
        public string Feature { get; set; }
        public string FeatureValue { get; set; }
    }
}
