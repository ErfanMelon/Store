using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Services.HomePages.Commands.AddBanner;
using Store.Application.Services.HomePages.Commands.ChangeBannerState;
using Store.Application.Services.HomePages.Commands.DeleteBanner;
using Store.Application.Services.HomePages.Queries.GetBanners;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.Application.Services.HomePages.Facade
{
    public class HomePageFacade:IHomePageFacade
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;
        public HomePageFacade(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }
        
        private IAddBannerService _addBannerService;
        public IAddBannerService addBannerService
        {
            get
            {
                return _addBannerService = _addBannerService ?? new AddBannerService(_context, _uploadFileService);
            }
        }

        private IGetBannersService _getBannersService ;
        public IGetBannersService getBannersService
        {
            get
            {
                return _getBannersService = _getBannersService ?? new GetBannersService(_context);
            }
        }

        private IDeleteBannerService _deleteBannerService;
        public IDeleteBannerService deleteBannerService
        {
            get
            {
                return _deleteBannerService = _deleteBannerService ?? new DeleteBannerService(_context);
            }
        }

        private IGetBannersSiteService _getBannersSiteService;
        public IGetBannersSiteService getBannersSiteService
        {
            get
            {
                return _getBannersSiteService = _getBannersSiteService ?? new GetBannersSiteService(_context);
            }
        }
        private IChangeBannerStateService _changeBannerStateService;
        public IChangeBannerStateService changeBannerStateService
        {
            get
            {
                return _changeBannerStateService = _changeBannerStateService ?? new ChangeBannerStateService(_context);
            }
        }
    }
}
