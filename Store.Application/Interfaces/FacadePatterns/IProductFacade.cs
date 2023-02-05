using Store.Application.Services.Products.Commands.AddBrand;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.AddComment;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.DeleteBrandService;
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

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IProductFacade
    {
        public IGetProductsSite getProductsSite { get; }
        public IGetProductSiteService getProductSiteService { get; }
        public IAddCommentService addCommentService { get; }
        public IGetProductCommentsService getProductCommentsService { get; }
    }
}
