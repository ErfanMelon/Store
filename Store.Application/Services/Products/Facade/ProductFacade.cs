using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.DeleteCategory;
using Store.Application.Services.Products.Commands.DeleteProduct;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetCategory;
using Store.Application.Services.Products.Queries.GetProductAdmin;
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
        public ProductFacade(IDataBaseContext context,IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        private IAddCategoryService _addCategoryService;
        public IAddCategoryService addCategoryService
        {
            get
            {
                return _addCategoryService = _addCategoryService ?? new AddCategoryService(_context);
            }
        }

        private IGetCategoriesService _getCategoriesService;
        public IGetCategoriesService getCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);
            }
        }

        private IEditCategoryService _editCategoryService;
        public IEditCategoryService editCategoryService
        {
            get
            {
                return _editCategoryService = _editCategoryService ?? new EditCategoryService(_context);
            }
        }

        private IDeleteCategoryService _deleteCategoryService;
        public IDeleteCategoryService deleteCategoryService
        {
            get
            {
                return _deleteCategoryService = _deleteCategoryService ?? new DeleteCategoryService(_context);
            }
        }
        private IGetCategoryDetailService _getCategoryDetailService;
        public IGetCategoryDetailService getCategoryDetailService
        {
            get
            {
                return _getCategoryDetailService = _getCategoryDetailService ?? new GetCategoryDetailService(_context);
            }
        }
        private IAddProductService _addProductService;
        public IAddProductService addProductService
        {
            get
            {
                return _addProductService = _addProductService ?? new AddProductService(_context, _hostingEnvironment);
            }
        }

        private IGetProductsAdminService _getProductsAdminService;
        public IGetProductsAdminService getProductsAdminService
        {
            get
            {
                return _getProductsAdminService = _getProductsAdminService ?? new GetProductsAdminService(_context);
            }
        }
        private IDeleteProductService _deleteProductService;
        public IDeleteProductService deleteProductService
        {
            get
            {
                return _deleteProductService = _deleteProductService ?? new DeleteProductService(_context);
            }
        }

        private IGetProductAdminService _getProductAdminService;
        public IGetProductAdminService getProductAdminService
        {
            get
            {
                return _getProductAdminService = _getProductAdminService ?? new GetProductAdminService(_context);
            }
        }
        private IEditProductService _editProductService;
        public IEditProductService editProductService
        {
            get
            {
                return _editProductService = _editProductService ?? new EditProductService(_context, _hostingEnvironment);
            }
        }
        private IGetProductEditService _getProductEditService;
        public IGetProductEditService getProductEditService
        {
            get
            {
                return _getProductEditService = _getProductEditService ?? new GetProductEditService(_context);
            }
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
    }
}
