using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.AddBanner
{
    public interface IAddBannerService
    {
        ResultDto Execute(RequestBannerDto request);
    }
}
