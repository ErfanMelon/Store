using MediatR;
using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
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
        private readonly IMediator _mediator;
        public HomePageFacade(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        private IAddBannerService _addBannerService;
        public IAddBannerService addBannerService
        {
            get
            {
                return _addBannerService = _addBannerService ?? new AddBannerService(_context,_mediator);
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
