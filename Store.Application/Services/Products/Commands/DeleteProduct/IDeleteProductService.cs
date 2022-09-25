using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteProduct
{
    public interface IDeleteProductService
    {
        ResultDto Execute(long productid);
    }
}
