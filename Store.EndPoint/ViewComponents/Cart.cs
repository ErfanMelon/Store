using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts.Queries.GetCart;
using Store.EndPoint.Tools;

namespace Store.EndPoint.ViewComponents;

public class Cart:ViewComponent
{
    private readonly IMediator _mediator;
    private readonly ICookieManager _cookieManager;
    public Cart(IMediator mediator, ICookieManager cookieManager)
    {
        _mediator = mediator;
        _cookieManager = cookieManager;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = ClaimTool.GetUserId(HttpContext.User);
        var browserId = _cookieManager.GetBrowserId(HttpContext);
        var result = await _mediator.Send(new GetCartQuery(browserId, userId));
        return View("Cart", result);
    }
}
