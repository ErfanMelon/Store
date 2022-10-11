using Store.Application.Services.HomePages.Commands.AddBanner;
using Store.Application.Services.HomePages.Commands.ChangeBannerState;
using Store.Application.Services.HomePages.Commands.DeleteBanner;
using Store.Application.Services.HomePages.Queries.GetBanners;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IHomePageFacade
    {
        public IAddBannerService addBannerService { get; }
        public IGetBannersService getBannersService { get; }
        public IDeleteBannerService deleteBannerService { get; }
        public IGetBannersSiteService getBannersSiteService { get; }
        public IChangeBannerStateService changeBannerStateService { get; }
    }
}
