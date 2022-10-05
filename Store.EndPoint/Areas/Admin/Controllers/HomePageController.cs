using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Commands.AddBanner;
using Store.Application.Services.HomePages.Commands.DeleteBanner;
using Store.Application.Services.HomePages.Queries.GetBanners;
using Store.Domain.Entities.HomePages;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IAddBannerService _addBannerService;
        private readonly IGetBannersService _getBannersService;
        private readonly IDeleteBannerService _deleteBannerService;
        public HomePageController(IAddBannerService addBannerService, IGetBannersService getBannersService,IDeleteBannerService deleteBannerService)
        {
            _addBannerService = addBannerService;
            _getBannersService = getBannersService;
            _deleteBannerService = deleteBannerService;
        }

        public IActionResult AddBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBanner(string bannerLink)
        {
            RequestBannerDto bannerDto = new RequestBannerDto
            {
                Link = bannerLink,
                Image = Request.Form.Files.FirstOrDefault(),
            };
            var result = _addBannerService.Execute(bannerDto);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Index()
        {
            return View(_getBannersService.Execute().Data);
        }
        [HttpPost]
        public IActionResult DeleteBanner(int bannerId)
        {
            return Json(_deleteBannerService.Execute(bannerId));
        }
    }
}
