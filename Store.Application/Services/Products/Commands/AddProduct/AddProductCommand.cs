using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using static Store.Common.UploadPath;

namespace Store.Application.Services.Products.Commands.AddProduct;
public class RequestFeatureDto
{
    public string Feature { get; set; }
    public string Value { get; set; }
}
public class AddProductCommand : IRequest<ResultDto>
{
    public string ProductTitle { get; set; }
    public int BrandId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Inventory { get; set; }
    public bool Displayed { get; set; } = true;
    public long CategoryId { get; set; }
    [FromBody]
    public IFormFileCollection Images { get; set; }
    public List<RequestFeatureDto> ProductFeatures { get; set; }
    public AddProductCommand()
    {

    }
    public class Validator : AbstractValidator<AddProductCommand>
    {
        public Validator()
        {
            RuleFor(e => e.ProductTitle).NotEmpty().WithMessage("عنوان محصول نمیتواند خالی باشد !");
            RuleFor(e => e.BrandId).GreaterThan(0).WithMessage("برند محصول معتبر نیست");
            RuleFor(e => e.CategoryId).GreaterThan(0).WithMessage("دسته بندی محصول معتبر نیست");
            RuleFor(e => e.Description).NotEmpty().WithMessage("توضیحات نمیتواند خالی باشد !");
            RuleFor(e => e.Price).GreaterThan(0).WithMessage("مبلغ وارد شده صحیح نمیباشد !");
            RuleFor(e => e.Inventory).GreaterThan(0).WithMessage("موجودی صحیح نمیباشد !");
            RuleFor(e => e.ProductFeatures).NotEmpty().WithMessage("ویژگی های محصول معتبر نیست");
            RuleForEach(e => e.ProductFeatures).ChildRules(productfeature =>
            {
                productfeature.RuleFor(p => p.Feature).NotEmpty().WithMessage("عنوان ویژگی را وارد کنید !");
                productfeature.RuleFor(p => p.Value).NotEmpty().WithMessage("مقدار ویژگی را وارد کنید !");
            });
            RuleFor(e => e.Images).NotEmpty().WithMessage("حداقل یک عکس محصول آپلود کنید");
        }
    }
    public class Handler : IRequestHandler<AddProductCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;

        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category is null)
                throw new ArgumentNullException("دسته بندی محصول معتبر نیست");

            var brand = await _context.ProductBrands.FindAsync(request.BrandId);
            if (brand is null)
                throw new ArgumentNullException("برند محصول معتبر نیست");

            Product product = new Product()
            {
                Category = category,
                Brand = brand,
                Views = 0,
                Description = request.Description,
                Displayed = request.Displayed,
                Inventory = request.Inventory,
                Price = request.Price,
                ProductTitle = request.ProductTitle,
            };
            _context.Products.Add(product);

            // Add Product's Images
            var images = new List<ProductImages>();
            foreach (var item in request.Images)
            {
                var uploadedimg = await _mediator.Send(new UploadFileCommand(item, UploadFileType.ProductImage));
                images.Add(new ProductImages
                {
                    Product = product,
                    Src = uploadedimg.FileNameAddress
                });
            }
            await _context.ProductImages.AddRangeAsync(images);

            // Add Product's Features
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
            await _context.ProductFeatures.AddRangeAsync(features);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $"{product.ProductTitle} با موفقیت ثبت شد !");
        }
    }
}

