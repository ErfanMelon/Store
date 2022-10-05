using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Commands.AddVisitBanner;
using Store.Application.Services.HomePages.Queries.GetBannersSite;
using Store.EndPoint.Models;
using System.Diagnostics;

namespace Store.EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetBannersSiteService _getBannersSiteService;
        private readonly IAddVisitBannerService _addVisitBannerService;

        public HomeController(ILogger<HomeController> logger,IGetBannersSiteService getBannersSiteService, IAddVisitBannerService addVisitBannerService)
        {
            _logger = logger;
            _getBannersSiteService = getBannersSiteService;
            _addVisitBannerService = addVisitBannerService;
        }
        public IActionResult Index()
        {
            //return RedirectToAction("Index", "Product");
            return View(_getBannersSiteService.Execute().Data);
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