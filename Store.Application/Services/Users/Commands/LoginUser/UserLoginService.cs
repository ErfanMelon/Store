using Store.Application.Interfaces.Context;
using Store.Application.Validations.User;
using Store.Common;
using Store.Common.Dto;
using Store.Common.Roles;

namespace Store.Application.Services.Users.Commands.LoginUser
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public UserLoginService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ResultDto<UserLoginDto> Execute(RequestLoginDto request)
        {
            var ValidationRules = new LoginValidation();
            var validate=ValidationRules.Validate(request);
            if (!validate.IsValid)
            {
                return new ResultDto<UserLoginDto> { Message = validate.Errors[0].ErrorMessage };
            }

            var user = _dataBaseContext.Users
                .Where(p => p.Email.Equals(request.Username)
            && p.IsActive == true)
            .FirstOrDefault();

            if (user == null)
            {
                return new ResultDto<UserLoginDto>
                {
                    Message = "کاربری با این ایمیل در سایت ثبت نام نکرده است",
                };
            }

            var passwordHasher = new PasswordHasher();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, request.Password);
            if (resultVerifyPassword == false)
            {
                return new ResultDto<UserLoginDto>
                {
                    Message = "رمز وارد شده اشتباه است!",
                };
            }
            return new ResultDto<UserLoginDto>
            {
                Data = new UserLoginDto
                {
                    Role = Enum.GetName(typeof(BaseRoles), user.RoleId),
                    UserId = user.UserId,
                    Name = user.UserFullName
                },
                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد",
            };


        }
    }
}
