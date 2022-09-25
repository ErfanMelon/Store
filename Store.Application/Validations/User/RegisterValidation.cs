using FluentValidation;
using Store.Application.Services.Users.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Validations.User
{
    public class RegisterValidation:AbstractValidator<RegisterUserDto>
    {
        public RegisterValidation()
        {
            RuleFor(e=>e.Email).NotEmpty().EmailAddress().WithMessage("ایمیل نامعتبر است !");
            RuleFor(e => e.FullName).NotEmpty().WithMessage("نام را به درستی وارد کنید !");
            RuleForEach(e => e.RoleIds).NotEmpty().WithMessage("نقش را وارد کنید !");
            RuleFor(e => e.Password).NotNull().MinimumLength(8).WithMessage("رمز باید حداقل طول 8 کاراکتر باشد !");
            RuleFor(e => e.RePassword).Equal(e => e.Password).WithMessage("رمز عبور و تکرار آن برابر نیست !");
        }
    }
}
