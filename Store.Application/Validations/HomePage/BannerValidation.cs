using FluentValidation;
using Store.Application.Services.HomePages.Commands.AddBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Validations.HomePage
{
    public class BannerValidation:AbstractValidator<RequestBannerDto>
    {
        public BannerValidation()
        {
            RuleFor(e=>e.Image).NotEmpty().WithMessage("لطفا تصویر را وارد کنید !");
        }
    }
}
