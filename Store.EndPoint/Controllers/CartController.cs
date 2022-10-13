using Store.EndPoint.Tools;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;

namespace Store.EndPoint.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICookieManager _cookieManager;
        public CartController(ICartService cartService,ICookieManager cookieManager)
        {
            _cartService = cartService;
            _cookieManager = cookieManager;
        }
        public IActionResult Index()
        {
            Guid browserid = _cookieManager.GetBrowserId(HttpContext);
            if (User.Identity.IsAuthenticated)
            {
                long userId = ClaimTool.GetUserId(User).Value;
                _cartService.JoinCartToUser(browserid, userId);
                return View(_cartService.GetCart(userId).Data);
            }
            return View(_cartService.GetCart(browserid).Data);
        }
        public IActionResult Add(long id)
        {
            Guid browserid = _cookieManager.GetBrowserId(HttpContext);
            _cartService.EditProductCart(browserid, id, 1);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(long id)
        {
            Guid browserid = _cookieManager.GetBrowserId(HttpContext);
            _cartService.EditProductCart(browserid, id, -1);
            return RedirectToAction("Index");
        }
        public IActionResult AddToCart(long productid,short count)
        {
            Guid browserid = _cookieManager.GetBrowserId(HttpContext);
            _cartService.AddToCart(browserid, productid,count);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(long productid)
        {
            Guid browserid = _cookieManager.GetBrowserId(HttpContext);
            _cartService.DeleteFromCart(browserid, productid);
            return RedirectToAction("Index");
        }
    }
}
