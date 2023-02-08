using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.EndPoint.ViewComponents;

    public class RightBanner : ViewComponent
    {
        private readonly IMediator _mediator;

        public RightBanner(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rightBanners = await _mediator.Send(new GetBannerSiteQuery(1, Domain.Entities.HomePages.BannerLocation.R1));
            return View("RightBanner", rightBanners);
        }
    }

