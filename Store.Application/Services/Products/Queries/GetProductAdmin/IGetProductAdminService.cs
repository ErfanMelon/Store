using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductAdmin
{
    public interface IGetProductAdminService
    {
        ResultDto<GetProductAdminDto> Execute(long productid);
    }
}
