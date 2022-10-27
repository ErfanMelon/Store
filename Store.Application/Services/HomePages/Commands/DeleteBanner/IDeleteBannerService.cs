using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.DeleteBanner
{
    public interface IDeleteBannerService
    {
        ResultDto Execute(int bannerId);
    }
}
