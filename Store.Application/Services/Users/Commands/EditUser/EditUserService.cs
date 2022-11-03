using Store.Application.Interfaces.Context;
using Store.Application.Validations.User;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public EditUserService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ResultDto<long> Execute(EditUserDto request)
        {
            var ValidationRules = new EditUserValidation();
            var IsValidUserRequest = ValidationRules.Validate(request);
            if (!IsValidUserRequest.IsValid)
            {
                return new ResultDto<long> { Message = IsValidUserRequest.Errors[0].ErrorMessage };
            }

            var user = _dataBaseContext.Users.Where(u => u.UserId == request.UserId)
                .FirstOrDefault();
            if (user != null)
            {
                user.UserFullName = request.FullName;
                user.Email = request.Email;
                user.UpdateTime = DateTime.Now;
                user.Role = _dataBaseContext.Roles.Find(request.RoleId);
                user.Address = request.Address;
                user.PhoneNumber = request.PhoneNumber;
                user.ZipCode = request.ZipCode;
                _dataBaseContext.SaveChanges();
                return new ResultDto<long>
                {
                    Data = user.UserId,
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ویرایش شد !"
                };
            }
            // else
            return new ResultDto<long>
            {
                Message = "کاربری پیدا نشد !"
            };


        }
    }

}

