using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.AddBrand
{
    public interface IAddBrandService
    {
        ResultDto Execute(string BrandName);
    }
    public class AddBrandService : IAddBrandService
    {
        private readonly IDataBaseContext _context;
        public AddBrandService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string BrandName)
        {
                if (!string.IsNullOrWhiteSpace(BrandName))
                {
                    _context.ProductBrands.Add(new Domain.Entities.Products.ProductBrand { Brand = BrandName });
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true, Message = $"{BrandName} با موفقیت ثبت شد !" };
                }
                return new ResultDto { Message = "نام برند را به درستی وارد کنید !" };
        }
    }
}
