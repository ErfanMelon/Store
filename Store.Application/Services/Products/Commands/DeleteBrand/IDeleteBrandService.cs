using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.DeleteBrandService
{
    public interface IDeleteBrandService
    {
        ResultDto Execute(int brandId);
    }
    public class DeleteBrandService : IDeleteBrandService
    {
        private readonly IDataBaseContext _context;
        public DeleteBrandService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int brandId)
        {
            var brand = _context.ProductBrands.Find(brandId);
            if (brand != null)
            {
                brand.IsRemoved = true;
                brand.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"{brand.Brand} با موفقیت حذف شد !" };
            }
            return new ResultDto { Message = "برند یافت نشد !" };

        }
    }
}
