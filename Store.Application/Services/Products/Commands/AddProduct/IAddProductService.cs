using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.AddProduct
{
    public interface IAddProductService
    {
        ResultDto Execute(RequestProductDto request);
    }
   
}
