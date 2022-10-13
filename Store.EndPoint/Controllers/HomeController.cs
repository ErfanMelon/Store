using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.HomePages.Queries.GetBannersSite;
using Store.EndPoint.Models;
using System.Diagnostics;

namespace Store.EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageFacade _homePageFacade;
        private readonly IProductFacade _productFacade;
        public HomeController(ILogger<HomeController> logger,IHomePageFacade homePageFacade, IProductFacade productFacade)
        {
            _logger = logger;
            _homePageFacade = homePageFacade;
            _productFacade = productFacade;
        }
        public IActionResult Index()
        {
            ViewBag.Categories = _productFacade.getCategoriesService.Execute().Data.Select(c=>c.CategoryId).ToList();
            //return RedirectToAction("Index", "Product");
            return View(_homePageFacade.getBannersSiteService.Execute().Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}