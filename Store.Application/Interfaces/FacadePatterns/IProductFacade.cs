using Store.Application.Services.Products.Commands.AddBrand;
using Store.Application.Services.Products.Commands.AddCategory;
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
using Store.Application.Services.Products.Queries.GetProductForEdit;
using Store.Application.Services.Products.Queries.GetProductsAdmin;
using Store.Application.Services.Products.Queries.GetProductSite;
using Store.Application.Services.Products.Queries.GetProductsSite;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IProductFacade
    {
        public IAddCategoryService addCategoryService { get; }
        public IGetCategoriesService getCategoriesService { get; }
        public IEditCategoryService editCategoryService { get; }
        public IDeleteCategoryService deleteCategoryService { get; }
        public IGetCategoryDetailService getCategoryDetailService { get; }
        public IAddProductService addProductService { get; }
        public IGetProductsAdminService getProductsAdminService { get; }
        public IDeleteProductService deleteProductService { get; }
        public IGetProductAdminService getProductAdminService { get; }
        public IEditProductService editProductService { get; }
        public IGetProductEditService getProductEditService { get; }
        public IGetProductsSite getProductsSite { get; }
        public IGetProductSiteService getProductSiteService { get; }
        public IAddBrandService addBrandService { get; }
        public IDeleteBrandService deleteBrandService { get; }
        public IGetBrandsService getBrandsService { get; }
    }
}
