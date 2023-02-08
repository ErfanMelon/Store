using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.EndPoint.ViewComponents;

public class LeftBanner : ViewComponent
{
    private readonly IMediator _mediator;

    public LeftBanner(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var leftBanners = await _mediator.Send(new GetBannerSiteQuery(2, Domain.Entities.HomePages.BannerLocation.L1));
        return View("LeftBanner", leftBanners);
    }
}
