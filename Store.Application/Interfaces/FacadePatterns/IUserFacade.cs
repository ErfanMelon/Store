using Store.Application.Services.Users.Commands.ChangeUserState;
using Store.Application.Services.Users.Commands.DeleteUser;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.LoginUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUsers;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IUserFacade
    {
        public IChangeUserStateService changeUserStateService { get; }
        public IDeleteUserService  deleteUserService { get; }
        public IEditUserService editUserService { get; }
        public IUserLoginService userLoginService { get; }
        public IRegisterUserService registerUserService { get; }
        public IGetRolesService getRolesService { get; }
        public IGetUserService getUserService { get; }
    }
}
