using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.LoginUser
{
    public interface IUserLoginService
    {
        ResultDto<UserLoginDto> Execute(RequestLoginDto request);
    }
}
