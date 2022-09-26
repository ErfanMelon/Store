using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Context;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddProduct
{
    public class AddProductService : IAddProductService
    {
        private readonly IDataBaseContext _dataBaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddProductService(IDataBaseContext dataBaseContext, IHostingEnvironment hostingEnvironment)
        {
            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultDto Execute(RequestProductDto request)
        {
            RequestProductDtoValidation validations = new RequestProductDtoValidation();
            var resultValidation = validations.Validate(request);
            if (!resultValidation.IsValid)
            {
                return new ResultDto { Message = resultValidation.Errors[0].ErrorMessage };
            }
            try
            {
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

                var images = new List<ProductImages>();
                foreach (var item in request.Images)
                {
                    var uploadedimg = UploadFile(item);
                    images.Add(new ProductImages
                    {
                        Product = product,
                        Src = uploadedimg.FileNameAddress
                    });
                }

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
            catch (Exception)
            {
                return new ResultDtoError();
            }
        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }

}
