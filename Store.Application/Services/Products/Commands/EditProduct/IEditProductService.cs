using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.DeleteFile;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using static Store.Common.UploadPath;

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
        private readonly IMediator _mediator;
        public EditProductService(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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
                _mediator.Send(new DeleteFileCommand(item.Src));
                _context.ProductImages.Remove(item);
            }
            var images = new List<ProductImages>();
            foreach (var item in request.Images)//add new
            {
                var srcimg = _mediator.Send(new UploadFileCommand(item, UploadFileType.ProductImage)).Result;
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
