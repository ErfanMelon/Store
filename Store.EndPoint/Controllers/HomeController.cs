using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.EndPoint.Models;
using System.Diagnostics;

namespace Store.EndPoint.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;
    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> Index()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());
        ViewBag.Categories = categories.Data.Select(c => c.CategoryId).ToList();
        return View();
    }

    public IActionResult Privacy() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}