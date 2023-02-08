using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.EndPoint.ViewComponents;

public class GroundBanner : ViewComponent
{
    private readonly IMediator _mediator;

    public GroundBanner(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var groundBanners = await _mediator.Send(new GetBannerSiteQuery(10, Domain.Entities.HomePages.BannerLocation.G1));
        return View("GroundBanner", groundBanners);
    }
}
