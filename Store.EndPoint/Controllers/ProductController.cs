using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddComment;
using Store.Application.Services.Products.Queries.GetProductSite;
using Store.Application.Services.Products.Queries.GetProductsSite;
using Store.Common;
using Store.EndPoint.Models;
using Store.EndPoint.Tools;
using System.ComponentModel.DataAnnotations;
using Arrange = Store.Application.Services.Products.Queries.GetProductsSite.Order;

namespace Store.EndPoint.Controllers;

public class ProductController : Controller
{
    private readonly IProductFacade _productFacade;
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator, IProductFacade productFacade)
    {
        _mediator = mediator;
        _productFacade = productFacade;
    }

    private static SelectList productRate = GetProductRate();
    public async Task<IActionResult> Index(string SearchKey, int page = 1, long? CategoryId = null, int pagesize = 10, Arrange ordering = Arrange.NotOrdered)
    {
        var result = await _mediator.Send(new GetProductsSiteQuery(page, pagesize, CategoryId, SearchKey, ordering));

        return View(result);
    }
    public async Task<IActionResult> Detail(long id)
    {
        ViewBag.RateOptions = productRate;
        var result = await _mediator.Send(new GetProductSiteQuery(id));
        if (result.IsSuccess)
            return View(result.Data);
        return BadRequest();
    }
    enum ProductRate : int
    {
        [Display(Name = "خیلی بد")]
        Awful = 1,
        [Display(Name = "بد")]
        Bad = 2,
        [Display(Name = "متوسط")]
        Normal = 3,
        [Display(Name = "خوب")]
        Good = 4,
        [Display(Name = "عالی")]
        Best = 5
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
            Comment = model.Comment,
            CommentTitle = model.CommentTitle,
            Cons = model.Cons,
            ProductId = model.ProductId,
            Pros = model.Pros,
            Stars = Convert.ToInt16(model.Stars),
            UserId = userId.Value
        };
        var result = _productFacade.addCommentService.Execute(comment);
        return Json(result);
    }
}
