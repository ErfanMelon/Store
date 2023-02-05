using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.DeleteFile;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using static Store.Common.UploadPath;

namespace Store.Application.Services.Products.Commands.EditProduct;
public class EditProductCommand : IRequest<ResultDto>
{
    public long ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int BrandId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Inventory { get; set; }
    public bool Displayed { get; set; }
    public long CategoryId { get; set; }
    public IFormFileCollection Images { get; set; }
    public List<RequestFeatureDto> ProductFeatures { get; set; }

    public class Validator : AbstractValidator<EditProductCommand>
    {
        public Validator()
        {
            RuleFor(e => e.ProductTitle).NotEmpty().WithMessage("عنوان محصول نمیتواند خالی باشد !");
            RuleFor(e => e.BrandId).GreaterThan(0).WithMessage("برند محصول معتبر نیست");
            RuleFor(e => e.CategoryId).GreaterThan(0).WithMessage("دسته بندی محصول معتبر نیست");
            RuleFor(e => e.Description).NotEmpty().WithMessage("توضیحات نمیتواند خالی باشد !");
            RuleFor(e => e.Price).GreaterThan(0).WithMessage("مبلغ وارد شده صحیح نمیباشد !");
            RuleFor(e => e.Inventory).GreaterThan(0).WithMessage("موجودی صحیح نمیباشد !");

            RuleForEach(e => e.ProductFeatures).ChildRules(productfeature =>
            {
                productfeature.RuleFor(p => p.Feature).NotEmpty().WithMessage("عنوان ویژگی را وارد کنید !");
                productfeature.RuleFor(p => p.Value).NotEmpty().WithMessage("مقدار ویژگی را وارد کنید !");
            }).When(e => e.ProductFeatures != null);
            RuleFor(e => e.ProductId).NotEmpty().NotEqual(0).WithErrorCode("محصول پیدا نشد !");
            RuleFor(e => e.Images).NotEmpty().WithMessage("حداقل یک عکس محصول آپلود کنید");
        }
    }

    public class Handler : IRequestHandler<EditProductCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;
        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductFeatures)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product is null)
                throw new ArgumentNullException("محصول معتبر نیست");

            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category is null)
                throw new ArgumentNullException("دسته بندی محصول معتبر نیست");

            var brand = await _context.ProductBrands.FindAsync(request.BrandId);
            if (brand is null)
                throw new ArgumentNullException("برند محصول معتبر نیست");

            product.ProductTitle = request.ProductTitle;
            product.Brand = brand;
            product.Category = category;
            product.Inventory = request.Inventory;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Displayed = request.Displayed;

            // Update Images

            foreach (var image in product.ProductImages)//Delete Old Images
            {
                await _mediator.Send(new DeleteFileCommand(image.Src));
                _context.ProductImages.Remove(image);
            }

            var images = new List<ProductImages>();
            foreach (var item in request.Images)//add new
            {
                var srcimg = await _mediator.Send(new UploadFileCommand(item, UploadFileType.ProductImage));
                if (srcimg.Status)
                    images.Add(new ProductImages
                    {
                        Product = product,
                        Src = srcimg.FileNameAddress,
                    });
            }
            await _context.ProductImages.AddRangeAsync(images);

            _context.ProductFeatures.RemoveRange(product.ProductFeatures);

            await _context.ProductFeatures
                .AddRangeAsync(request.ProductFeatures.Select(f => new ProductFeatures
                {
                    Feature = f.Feature,
                    FeatureValue = f.Value,
                    Product = product
                }));

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $"{product.ProductTitle} با موفقیت ویرایش شد !");
        }
    }
}

