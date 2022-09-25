using Store.Application.Interfaces.Context;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Validations.User;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Users;
using System.Text.RegularExpressions;

namespace Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRegisterUserDto> Execute(RegisterUserDto request)
        {
            var validationRules = new RegisterValidation();
            var IsValidRequest=validationRules.Validate(request);
            if (!IsValidRequest.IsValid)
            {
                return new ResultDto<ResultRegisterUserDto> { Message = IsValidRequest.Errors[0].ErrorMessage };
            }
            try
            {
                var passhasher = new PasswordHasher();
                User user = new User
                {
                    Email = request.Email,
                    Password = passhasher.HashPassword(request.Password),
                    UserFullName = request.FullName,
                    IsActive=true,
                };

                var UserRoles = new List<Role>();

                foreach (var item in request.RoleIds)
                {
                    var role = _context.Roles.Find(item);
                    UserRoles.Add(role);
                }
                foreach (var item in UserRoles)
                {
                    _context.UserInRoles.Add(new UserInRole
                    {
                        Role = item,
                        User = user,
                    });
                }

                _context.SaveChanges();
                return new ResultDto<ResultRegisterUserDto>
                {
                    Data = new ResultRegisterUserDto { UserId = user.UserId },
                    IsSuccess = true,
                    Message = "کاربر با موفقیت اضافه شد !",
                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultRegisterUserDto>
                {
                    Message = "کاربر ثبت نشد !",
                };
            }

        }

    }
}
