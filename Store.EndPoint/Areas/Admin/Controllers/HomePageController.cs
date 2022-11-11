using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.HomePages.Commands.AddBanner;
using Store.Application.Services.HomePages.Commands.DeleteBanner;
using Store.Application.Services.HomePages.Queries.GetBanners;
using Store.Domain.Entities.HomePages;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageFacade _homePageFacade;
        public HomePageController(IHomePageFacade homePageFacade)
        {
            _homePageFacade = homePageFacade;
        }

        public IActionResult AddBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBanner(RequestBannerDto request)
        {
            request.Image = Request.Form.Files.FirstOrDefault();
            var result = _homePageFacade.addBannerService.Execute(request);
            if (result.IsSuccess)
            {
                return Redirect("/Admin/HomePage/Index");
            }
            return View();
        }
        public IActionResult Index(int page=1,int pagesize=30)
        {
            return View(_homePageFacade.getBannersService.Execute(page,pagesize).Data);
        }
        [HttpPost]
        public IActionResult DeleteBanner(int bannerId)
        {
            return Json(_homePageFacade.deleteBannerService.Execute(bannerId));
        }
        [HttpPost]
        public IActionResult ChangeBannerState(int bannerId)
        {
            return Json(_homePageFacade.changeBannerStateService.Execute(bannerId));
        }
    }
}
