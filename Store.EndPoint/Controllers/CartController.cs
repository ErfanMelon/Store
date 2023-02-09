using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts.Commands.AttachUserToCart;
using Store.Application.Services.Carts.Commands.EditCartProduct;
using Store.Application.Services.Carts.Queries.GetCart;
using Store.EndPoint.Tools;

namespace Store.EndPoint.Controllers;

public class CartController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICookieManager _cookieManager;
    public CartController(ICookieManager cookieManager, IMediator mediator)
    {
        _cookieManager = cookieManager;
        _mediator = mediator;
    }
    public async Task<IActionResult> Index()
    {
        Guid browserid = _cookieManager.GetBrowserId(HttpContext);
        var userId = ClaimTool.GetUserId(User);

        var cart = await _mediator.Send(new GetCartQuery(browserid, userId));
        return View(cart);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditCartProductCommand command)
    {
        command.BrowserId = _cookieManager.GetBrowserId(HttpContext);
        command.UserId = ClaimTool.GetUserId(User);

        var result = await _mediator.Send(command);
        return Json(result);
    }
    //public async Task<IActionResult> Add(long id)
    //{
    //    Guid browserid = _cookieManager.GetBrowserId(HttpContext);
    //    _cartService.EditProductCart(browserid, id, 1);
    //    return RedirectToAction("Index");
    //}
    //public async Task<IActionResult> Remove(long id)
    //{
    //    Guid browserid = _cookieManager.GetBrowserId(HttpContext);
    //    _cartService.EditProductCart(browserid, id, -1);
    //    return RedirectToAction("Index");
    //}
    //public async Task<IActionResult> AddToCart(long productid, short count)
    //{
    //    Guid browserid = _cookieManager.GetBrowserId(HttpContext);
    //    _cartService.AddToCart(browserid, productid, count);
    //    return RedirectToAction("Index");
    //}
    //public async Task<IActionResult> RemoveFromCart(long productid)
    //{
    //    Guid browserid = _cookieManager.GetBrowserId(HttpContext);
    //    _cartService.DeleteFromCart(browserid, productid);
    //    return RedirectToAction("Index");
    //}
}
