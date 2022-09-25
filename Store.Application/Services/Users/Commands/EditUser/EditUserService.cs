using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Validations.User;
using Store.Common.Dto;
using Store.Domain.Entities.Users;

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
            try
            {
                var user = _dataBaseContext.Users.Where(u => u.UserId == request.UserId)
                    .Include(u => u.UserInRoles)
                    .FirstOrDefault();
                if (user != null)
                {
                    if (user.UserInRoles.Any())
                        foreach (var roleinuser in user.UserInRoles)
                        {
                            roleinuser.IsRemoved = true;
                            roleinuser.RemoveTime = DateTime.Now;
                        }

                    // Update User's Roles
                    var userroles = new List<Role>();
                    foreach (var item in request.RoleIds)
                    {
                        var role = _dataBaseContext.Roles.Find(item);
                        userroles.Add(role);
                    }
                    foreach (var item in userroles)
                    {
                        _dataBaseContext.UserInRoles.Add(new UserInRole
                        {
                            Role = item,
                            User = user,
                        });
                    }

                    // Update User
                    user.UserFullName = request.FullName;
                    user.Email = request.Email;
                    user.UpdateTime = DateTime.Now;

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
            catch (Exception)
            {
                return new ResultDto<long>
                {
                    Message = "کاربر ویرایش نشد !",
                };
            }

        }
    }

}

