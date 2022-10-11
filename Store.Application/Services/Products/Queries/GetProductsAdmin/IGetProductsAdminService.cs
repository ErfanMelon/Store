using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductsAdmin
{
    public interface IGetProductsAdminService
    {
        ResultDto<GetProductsAdminDto> Execute(int page,int pagesize, string SearchKey);
    }
}
