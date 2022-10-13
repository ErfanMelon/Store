using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Carts;
using Store.EndPoint.Tools;

namespace Store.EndPoint.ViewComponents
{
    public class Cart:ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly ICookieManager _cookieManager;
        public Cart(ICartService cartService, ICookieManager cookieManager)
        {
            _cartService = cartService;
            _cookieManager = cookieManager;
        }
        public IViewComponentResult Invoke()
        {
            return View("Cart",_cartService.GetCart(_cookieManager.GetBrowserId(HttpContext)).Data);
        }
    }
}
