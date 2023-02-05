using MediatR;
using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.AddComment;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.DeleteCategory;
using Store.Application.Services.Products.Commands.DeleteProduct;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetBrands;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetCategory;
using Store.Application.Services.Products.Queries.GetProductAdmin;
using Store.Application.Services.Products.Queries.GetProductComments;
using Store.Application.Services.Products.Queries.GetProductForEdit;
using Store.Application.Services.Products.Queries.GetProductsAdmin;
using Store.Application.Services.Products.Queries.GetProductSite;
using Store.Application.Services.Products.Queries.GetProductsSite;

namespace Store.Application.Services.Products.Facade
{
    public class ProductFacade : IProductFacade
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMediator _mediator;
        public ProductFacade(IDataBaseContext context, IHostingEnvironment hostingEnvironment, IMediator mediator)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _mediator = mediator;
        }
        private IGetProductsSite _getProductsSite;
        public IGetProductsSite getProductsSite
        {
            get
            {
                return _getProductsSite = _getProductsSite ?? new GetProductsSite(_context);
            }
        }
        private IGetProductSiteService _getProductSiteService;
        public IGetProductSiteService getProductSiteService
        {
            get
            {
                return _getProductSiteService = _getProductSiteService ?? new GetProductSiteService(_context);
            }
        }
        private IAddCommentService _addCommentService;
        public IAddCommentService addCommentService
        {
            get
            {
                return _addCommentService = _addCommentService ?? new AddCommentService(_context);
            }
        }
        private IGetProductCommentsService _getProductCommentsService;
        public IGetProductCommentsService getProductCommentsService
        {
            get
            {
                return _getProductCommentsService = _getProductCommentsService ?? new GetProductCommentsService(_context);
            }
        }
    }
}
