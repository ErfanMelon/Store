using FluentValidation;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.EditProduct;

namespace Store.Application.Validations.Product
{
    public class RequestProductDtoValidation : AbstractValidator<RequestProductDto>
    {
        public RequestProductDtoValidation()
        {
            RuleFor(e => e.ProductTitle).NotEmpty().WithMessage("عنوان محصول نمیتواند خالی باشد !");
            RuleFor(e => e.BrandId).NotEmpty().WithMessage("مدل محصول نمیتواند خالی باشد !");
            RuleFor(e => e.Description).NotEmpty().WithMessage("توضیحات نمیتواند خالی باشد !");
            RuleFor(e => e.Price).GreaterThanOrEqualTo(0).WithMessage("مبلغ وارد شده صحیح نمیباشد !");
            RuleFor(e => e.Inventory).GreaterThanOrEqualTo(0).WithMessage("موجودی صحیح نمیباشد !");
            RuleFor(e => e.CategoryId).NotEmpty().WithMessage("دسته بندی را وارد کنید !");

            RuleForEach(e => e.ProductFeatures).ChildRules(productfeature =>
            {
                productfeature.RuleFor(p => p.Feature).NotEmpty().WithMessage("عنوان ویژگی را وارد کنید !");
                productfeature.RuleFor(p => p.Value).NotEmpty().WithMessage("مقدار ویژگی را وارد کنید !");
            }).When(e => e.ProductFeatures != null);
            RuleForEach(e => e.Images).NotEmpty().WithErrorCode("عکس را ارسال کنید !");
        }
    }
    public class RequestEditProductDtoValidation : AbstractValidator<RequestEditProductDto>
    {
        public RequestEditProductDtoValidation()
        {
            RuleFor(e => e.ProductTitle).NotEmpty().WithMessage("عنوان محصول نمیتواند خالی باشد !");
            RuleFor(e => e.BrandId).NotEmpty().WithMessage("مدل محصول نمیتواند خالی باشد !");
            RuleFor(e => e.Description).NotEmpty().WithMessage("توضیحات نمیتواند خالی باشد !");
            RuleFor(e => e.Price).GreaterThanOrEqualTo(0).WithMessage("مبلغ وارد شده صحیح نمیباشد !");
            RuleFor(e => e.Inventory).GreaterThanOrEqualTo(0).WithMessage("موجودی صحیح نمیباشد !");
            RuleFor(e => e.CategoryId).NotEmpty().WithMessage("دسته بندی را وارد کنید !");

            RuleForEach(e => e.ProductFeatures).ChildRules(productfeature =>
            {
                productfeature.RuleFor(p => p.Feature).NotEmpty().WithMessage("عنوان ویژگی را وارد کنید !");
                productfeature.RuleFor(p => p.Value).NotEmpty().WithMessage("مقدار ویژگی را وارد کنید !");
            }).When(e => e.ProductFeatures != null);
            RuleForEach(e => e.Images).NotEmpty().WithErrorCode("عکس را ارسال کنید !");
            RuleFor(e => e.ProductId).NotEmpty().NotEqual(0).WithErrorCode("محصول پیدا نشد !");
        }
    }
}
