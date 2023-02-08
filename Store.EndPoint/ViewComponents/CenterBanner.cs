using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.EndPoint.ViewComponents;

public class CenterBanner : ViewComponent
{
    private readonly IMediator _mediator;

    public CenterBanner(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var centerBanners = await _mediator.Send(new GetBannerSiteQuery(1, Domain.Entities.HomePages.BannerLocation.CenterFullScreen));
        return View("CenterBanner", centerBanners);
    }
}
