using Store.Application.Interfaces.Context;
using Store.Application.Validations.User;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Users;

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
            var IsValidRequest = validationRules.Validate(request);
            if (!IsValidRequest.IsValid)
            {
                return new ResultDto<ResultRegisterUserDto> { Message = IsValidRequest.Errors[0].ErrorMessage };
            }

            var passhasher = new PasswordHasher();
            User user = new User
            {
                Email = request.Email,
                Password = passhasher.HashPassword(request.Password),
                UserFullName = request.FullName,
                IsActive = true,
                Address = request.Address,
                Role = _context.Roles.Find(request.RoleId),
                PhoneNumber = request.PhoneNumber,
                ZipCode = request.ZipCode
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResultDto<ResultRegisterUserDto>
            {
                Data = new ResultRegisterUserDto { UserId = user.UserId },
                IsSuccess = true,
                Message = "کاربر با موفقیت اضافه شد !",
            };
        }

    }
}
