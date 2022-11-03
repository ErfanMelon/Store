using FluentValidation;
using Store.Application.Services.Users.Commands.RegisterUser;

namespace Store.Application.Validations.User
{
    public class RegisterValidation:AbstractValidator<RegisterUserDto>
    {
        public RegisterValidation()
        {
            RuleFor(e=>e.Email).NotEmpty().EmailAddress().WithMessage("ایمیل نامعتبر است !");
            RuleFor(e => e.FullName).NotEmpty().WithMessage("نام را به درستی وارد کنید !");
            RuleFor(e => e.RoleId).NotEqual(0).WithMessage("نقش را وارد کنید !");
            RuleFor(e => e.Password).NotNull().MinimumLength(8).WithMessage("رمز باید حداقل طول 8 کاراکتر باشد !");
            RuleFor(e => e.RePassword).Equal(e => e.Password).WithMessage("رمز عبور و تکرار آن برابر نیست !");
            RuleFor(e => e.PhoneNumber).Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$").When(p=>p.PhoneNumber!=null).WithMessage("شماره تلفن صحیح نمیباشد !");
            RuleFor(e => e.ZipCode).Matches(@"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b").When(p=>p.ZipCode!=null).WithMessage("کدپستی صحیح نمیباشد !");
            
        }
    }
}
