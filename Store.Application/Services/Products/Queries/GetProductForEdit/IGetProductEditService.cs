using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductForEdit
{
    public interface IGetProductEditService
    {
        ResultDto<RequestProductDto> Execute(long productId);
    }
    public class GetProductEditService : IGetProductEditService
    {
        private readonly IDataBaseContext _context;
        public GetProductEditService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<RequestProductDto> Execute(long productId)
        {

            
                var product = _context.Products.Where(p => p.ProductId == productId)
                    .Include(p => p.ProductFeatures)
                    .FirstOrDefault();
                if (product != null)
                    return new ResultDto<RequestProductDto>
                    {
                        Data = new RequestProductDto
                        {
                            BrandId=product.BrandId,
                            CategoryId = product.CategoryId,
                            Description = product.Description,
                            Displayed = product.Displayed,

                            Inventory = product.Inventory,
                            Price = product.Price,
                            ProductTitle = product.ProductTitle,
                            ProductFeatures = product.ProductFeatures.ToList().Select(f => new RequestFeatureDto
                            {
                                Feature = f.Feature,
                                Value = f.FeatureValue
                            }).ToList(),
                            
                        },
                        IsSuccess = true,
                        Message = ""
                    };
                return new ResultDto<RequestProductDto> { Message = "پیدا نشد !" };
           
        }
    }
}
