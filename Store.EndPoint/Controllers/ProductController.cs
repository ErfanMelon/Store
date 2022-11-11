using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddComment;
using Store.Common;
using Store.EndPoint.Models;
using Store.EndPoint.Tools;
using System.ComponentModel.DataAnnotations;
using Order = Store.Application.Services.Products.Queries.GetProductsSite.Order;

namespace Store.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacade _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        private static SelectList productRate=GetProductRate();
        public IActionResult Index(string SearchKey,int page = 1,long? CategoryId=null,int pagesize=10,Order ordering=Order.NotOrdered)
        {
            var Result = _productFacade.getProductsSite.Execute(page,CategoryId,SearchKey,pagesize,ordering);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
        public IActionResult Detail(long Id)
        {
            ViewBag.RateOptions = productRate;
            var Result=_productFacade.getProductSiteService.Execute(Id);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
        enum ProductRate:int
        {
            [Display(Name ="خیلی بد")]
            Awful=1,
            [Display(Name = "بد")]
            Bad =2,
            [Display(Name = "متوسط")]
            Normal =3,
            [Display(Name = "خوب")]
            Good =4,
            [Display(Name = "عالی")]
            Best =5
        }
        static SelectList GetProductRate()
        {
            var productRate = from ProductRate r in Enum.GetValues(typeof(ProductRate))
                              select new
                              {
                                  ID = (int)r,
                                  Name = EnumHelpers<ProductRate>.GetDisplayValue(r)
                              };
            return new SelectList(productRate, "ID", "Name");
        }
        [HttpPost]
        public IActionResult AddComment(CommentViewModel model)
        {
            var userId = ClaimTool.GetUserId(User);
            RequestAddComment comment = new RequestAddComment
            {
                Comment=model.Comment,
                CommentTitle=model.CommentTitle,
                Cons=model.Cons,
                ProductId=model.ProductId,
                Pros=model.Pros,
                Stars=Convert.ToInt16(model.Stars),
                UserId=userId.Value
            };
            var result = _productFacade.addCommentService.Execute(comment);
            return Json(result);
        }
    }
}
