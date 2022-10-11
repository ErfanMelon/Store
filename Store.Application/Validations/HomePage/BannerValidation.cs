using FluentValidation;
using Store.Application.Services.HomePages.Commands.AddBanner;

namespace Store.Application.Validations.HomePage
{
    public class BannerValidation:AbstractValidator<RequestBannerDto>
    {
        public BannerValidation()
        {
            RuleFor(e=>e.Image).NotEmpty().WithMessage("لطفا تصویر را وارد کنید !");
            RuleFor(e => e.BannerLocation).IsInEnum().WithErrorCode("موقعیت وارد شده صحیح نمیباشد !");
        }
    }
}
