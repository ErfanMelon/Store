using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Queries.GetBannersSite;

namespace Store.EndPoint.ViewComponents;

public class CarouselIndex : ViewComponent
{
    private readonly IMediator _mediator;

    public CarouselIndex(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var sliders = await _mediator.Send(new GetBannerSiteQuery(4, Domain.Entities.HomePages.BannerLocation.Slider));
        return View("CarouselIndex", sliders);
    }
}
