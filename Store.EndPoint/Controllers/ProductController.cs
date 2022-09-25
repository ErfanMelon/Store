﻿using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;

namespace Store.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacade _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult Index(string SearchKey,int page = 1,long? CategoryId=null)
        {
            var Result = _productFacade.getProductsSite.Execute(page,CategoryId,SearchKey);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
        public IActionResult Detail(long Id)
        {
            var Result=_productFacade.getProductSiteService.Execute(Id);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
    }
}