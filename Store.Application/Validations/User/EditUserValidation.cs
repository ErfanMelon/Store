using FluentValidation;
using Store.Application.Services.Users.Commands.EditUser;

namespace Store.Application.Validations.User
{
    public class EditUserValidation:AbstractValidator<EditUserDto>
    {
        public EditUserValidation()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress().WithMessage("ایمیل نامعتبر است !");
            RuleFor(e => e.FullName).NotEmpty().WithMessage("نام را به درستی وارد کنید !");
            RuleFor(e => e.RoleId).NotEqual(0).WithMessage("نقش را وارد کنید !");
            RuleFor(e => e.UserId).NotEqual(0).WithMessage("کاربری پیدا نشد !");
            RuleFor(e => e.PhoneNumber).Matches(@"^([0-9]{11})$").When(p => p.PhoneNumber != null).WithMessage("شماره تلفن صحیح نمیباشد !");
            RuleFor(e => e.ZipCode).Matches(@"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b").When(p => p.ZipCode != null).WithMessage("کدپستی صحیح نمیباشد !");
        }
    }
}
