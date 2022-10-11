using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.DeleteUser
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public DeleteUserService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ResultDto<long> Execute(long userId)
        {
            
                var user = _dataBaseContext.Users.Where(u=>u.UserId==userId)
                    .Include(u=>u.UserInRoles)
                    .FirstOrDefault();
                if (user != null)
                {
                    if(user.UserInRoles.Any())
                        foreach (var item in user.UserInRoles)
                        {
                            item.IsRemoved = true;
                            item.RemoveTime = DateTime.Now;
                        }
                    user.IsRemoved = true;
                    user.RemoveTime = DateTime.Now;
                    _dataBaseContext.SaveChanges();
                    
                    return new ResultDto<long> { Data = userId, IsSuccess = true, Message =$"{user.UserFullName} با موفقیت حذف شد !" };
                }
                return new ResultDto<long> { Message = "کاربری پیدا نشد !" };
           
        }
    }
}
