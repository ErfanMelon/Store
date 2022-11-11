using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductAdmin
{
    public class GetProductAdminService : IGetProductAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetProductAdminDto> Execute(long productid)
        {
           
                var product = _context.Products.Where(p => p.ProductId == productid)
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.ProductImages)
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .ThenInclude(p=>p.ParentCategory)
                    .FirstOrDefault();
                if (product != null)
                {
                    return new ResultDto<GetProductAdminDto>
                    {
                        Data = new GetProductAdminDto
                        {
                            ProductId=product.ProductId,
                            Brand = product.Brand.Brand,
                            Category = GetCategory(product.Category),
                            TotalViews=product.Views,
                            //Stars=GetStars(productid),
                            Stars= _context.Comments.Any(c => c.ProductId == productid)?(int)_context.Comments.Where(c=>c.ProductId==productid).Average(c=>c.Score):0,
                            Description = product.Description,
                            Displayed = product.Displayed,
                            Features = product.ProductFeatures.ToList().Select(f => new GetProductFeatureDto
                            {
                                Feature = f.Feature,
                                FeatureValue = f.FeatureValue,
                            }).ToList(),
                            Images = product.ProductImages.ToList().Select(i => i.Src).ToList(),
                            Inventory = product.Inventory,
                            Price = product.Price,
                            ProductTitle = product.ProductTitle,
                            OrderCount=_context.OrderDetails.Any(d=>d.ProductId==productid)? _context.OrderDetails.Where(d=>d.ProductId==productid).Sum(p=>p.Count):0
                        },
                        IsSuccess=true,
                    };
                }
                return new ResultDto<GetProductAdminDto> { Message = "محصولی پیدا نشد !" };
           
        }
        string GetCategory(Category category)
        {
            if (category.ParentCategory != null)
                return $"{GetCategory(category.ParentCategory)} - {category.CategoryTitle}";
            return category.CategoryTitle;
        }
        //int GetStars(long productid)
        //{
        //    var ListRate=_context.ProductLikes.Where(p => p.ProductId == productid).Select(p => p.Score);
        //    if (ListRate.Any())
        //    {
        //        return (int)Math.Round(ListRate.Average(), MidpointRounding.AwayFromZero);
        //    }
        //    return 0;
        //}
    }
}
