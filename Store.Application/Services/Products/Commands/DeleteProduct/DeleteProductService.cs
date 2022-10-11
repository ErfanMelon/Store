using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductService: IDeleteProductService
    {
        private readonly IDataBaseContext _context;
        public DeleteProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long productid)
        {
                var product = _context.Products.Where(p=>p.ProductId==productid)
                    .Include(p=>p.ProductImages)
                    .Include(p=>p.ProductFeatures)
                    .FirstOrDefault();
                if (product!=null)
                {
                    if(product.ProductImages.Any())
                        foreach (var item in product.ProductImages)
                        {
                            item.RemoveTime = DateTime.Now;
                            item.IsRemoved = true;
                        }
                    if(product.ProductFeatures.Any())
                        foreach (var item in product.ProductFeatures)
                        {
                            item.RemoveTime = DateTime.Now;
                            item.IsRemoved = true;
                        }
                    product.IsRemoved = true;
                    product.RemoveTime = DateTime.Now;
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true, Message = $" {product.ProductTitle} با موفقیت حذف شد !" };
                }
                return new ResultDto { Message = "داده ای پیدا نشد !" };
        }
    }
}
