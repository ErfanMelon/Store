using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.DeleteFile;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(RequestEditProductDto request);
    }
    public class RequestEditProductDto : RequestProductDto
    {
        public long ProductId { get; set; }
    }
    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;
        private readonly IDeleteFileService _deleteFileService;
        public EditProductService(IDataBaseContext context, IUploadFileService uploadFileService, IDeleteFileService deleteFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
            _deleteFileService = deleteFileService;
        }

        public ResultDto Execute(RequestEditProductDto request)
        {
            var Validation = new RequestEditProductDtoValidation();
            var Valid = Validation.Validate(request);
            if (!Valid.IsValid)
            {
                return new ResultDto { Message = Valid.Errors[0].ErrorMessage };
            }
                var product = _context.Products.Where(p => p.ProductId == request.ProductId)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .FirstOrDefault();
                // Update Operation

                product.ProductTitle = request.ProductTitle;
                product.Brand = _context.ProductBrands.Find(request.BrandId);
                product.Category = _context.Categories.Find(request.CategoryId);
                product.Inventory = request.Inventory;
                product.Price = request.Price;
                product.Description = request.Description;
                product.Displayed = request.Displayed;
                product.UpdateTime = DateTime.Now;

                // images Update

                foreach (var item in product.ProductImages)//delete olds
                {
                    if (_deleteFileService.Execute(item.Src).Status)
                        _context.ProductImages.Remove(item);
                }
                var images = new List<ProductImages>();
                foreach (var item in request.Images)//add new
                {
                    var srcimg = _uploadFileService.Execute(item,UploadFileType.ProductImage);
                    if (srcimg.Status)
                        images.Add(new ProductImages
                        {
                            Product = product,
                            Src = srcimg.FileNameAddress,
                        });
                }
                _context.ProductImages.AddRange(images);

                // feature Update 
                if (product.ProductFeatures.Any())
                    _context.ProductFeatures.RemoveRange(product.ProductFeatures);

                if (request.ProductFeatures.Any())
                    _context.ProductFeatures.AddRange(request.ProductFeatures.ToList().Select(f => new ProductFeatures
                    {
                        Feature = f.Feature,
                        FeatureValue = f.Value,
                        Product = product
                    }
                    ));

                //save
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"{product.ProductTitle} با موفقیت ویرایش شد !" };
        }
    }
}
