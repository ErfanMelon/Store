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
            RuleForEach(e => e.RoleIds).NotEmpty().WithMessage("نقش را وارد کنید !");
            RuleFor(e => e.UserId).NotEqual(0).WithMessage("کاربری پیدا نشد !");
        }
    }
}
