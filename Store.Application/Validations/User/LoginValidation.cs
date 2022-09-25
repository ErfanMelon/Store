using FluentValidation;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Users.Commands.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Validations.User
{
    public class LoginValidation:AbstractValidator<RequestLoginDto>
    {
        public LoginValidation()
        {
            RuleFor(e => e.Username).NotEmpty().WithMessage("نام کاربری را وارد کنید !");
            RuleFor(e => e.Password).NotEmpty().WithMessage("رمز عبور را وارد کنید !");
        }
    }
}
