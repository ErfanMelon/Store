using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.ChangeUserState
{
    public class ChangeUserStateService : IChangeUserStateService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public ChangeUserStateService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ResultDto<long> Execute(long userId)
        {
            try
            {
                var user = _dataBaseContext.Users.Find(userId);
                if (user != null)
                {
                    user.IsActive = !user.IsActive;
                    user.UpdateTime = DateTime.Now;
                    _dataBaseContext.SaveChanges();
                    string state = user.IsActive == true ? "فعال" : "غیر فعال";
                    return new ResultDto<long> { Data = user.UserId, IsSuccess = true, Message = $"وضعیت کاربر به {state} تغییر کرد !" };
                }
                return new ResultDto<long> { Message = "کاربری پیدا نشد !" };
            }
            catch (Exception)
            {

                return new ResultDto<long> { Message = "انجام نشد!" };

            }
        }
    }
}
