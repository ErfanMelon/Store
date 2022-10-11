using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddProduct
{
    public class AddProductService : IAddProductService
    {
        private readonly IDataBaseContext _dataBaseContext;
        private readonly IUploadFileService _uploadFileService;
        public AddProductService(IDataBaseContext dataBaseContext, IUploadFileService uploadFileService)
        {
            _dataBaseContext = dataBaseContext;
            _uploadFileService = uploadFileService;
        }

        public ResultDto Execute(RequestProductDto request)
        {
            //Validation
            RequestProductDtoValidation validations = new RequestProductDtoValidation();
            var resultValidation = validations.Validate(request);
            if (!resultValidation.IsValid)
            {
                return new ResultDto { Message = resultValidation.Errors[0].ErrorMessage };
            }
            //Insert Product
            Product product = new Product()
            {
                Category = _dataBaseContext.Categories.Find(request.CategoryId),
                Brand = _dataBaseContext.ProductBrands.Find(request.BrandId),
                Views = 0,
                Description = request.Description,
                Displayed = request.Displayed,
                Inventory = request.Inventory,
                Price = request.Price,
                ProductTitle = request.ProductTitle,
            };
            _dataBaseContext.Products.Add(product);
            // Add Product's Images (If user sent)
            var images = new List<ProductImages>();
            foreach (var item in request.Images)
            {
                var uploadedimg = _uploadFileService.Execute(item, UploadFileType.ProductImage);
                images.Add(new ProductImages
                {
                    Product = product,
                    Src = uploadedimg.FileNameAddress
                });
            }
            // Add Product's Features (If user sent)
            var features = new List<ProductFeatures>();
            foreach (var item in request.ProductFeatures)
            {
                features.Add(new ProductFeatures
                {
                    Feature = item.Feature,
                    FeatureValue = item.Value,
                    Product = product,
                });
            }

            if (images.Any())
                _dataBaseContext.ProductImages.AddRange(images);
            if (features.Any())
                _dataBaseContext.ProductFeatures.AddRange(features);

            _dataBaseContext.SaveChanges();
            return new ResultDto { IsSuccess = true, Message = $"{product.ProductTitle} با موفقیت ثبت شد !" };

        }
    }

}
