using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.ChangeBannerState
{
    public interface IChangeBannerStateService
    {
        ResultDto Execute(int bannerId);
    }
}
