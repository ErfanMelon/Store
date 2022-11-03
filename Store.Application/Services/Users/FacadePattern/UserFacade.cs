using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Users.Commands.ChangeUserState;
using Store.Application.Services.Users.Commands.DeleteUser;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.LoginUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Queries.GetUserDetail;
using Store.Application.Services.Users.Queries.GetUsers;

namespace Store.Application.Services.Users.FacadePattern
{
    public class UserFacade : IUserFacade
    {
        private readonly IDataBaseContext _context;
        public UserFacade(IDataBaseContext context)
        {
            _context = context;
        }

        private IChangeUserStateService _changeUserStateService;
        public IChangeUserStateService changeUserStateService
        {
            get
            {
                return _changeUserStateService = _changeUserStateService ?? new ChangeUserStateService(_context);
            }
        }

        private IDeleteUserService _deleteUserService;
        public IDeleteUserService deleteUserService
        {
            get
            {
                return _deleteUserService = _deleteUserService ?? new DeleteUserService(_context);
            }
        }

        private IEditUserService _editUserService;
        public IEditUserService editUserService
        {
            get
            {
                return _editUserService = _editUserService ?? new EditUserService(_context);
            }
        }
        private IUserLoginService _userLoginService;
        public IUserLoginService userLoginService
        {
            get
            {
                return _userLoginService = _userLoginService ?? new UserLoginService(_context);
            }
        }
        private IRegisterUserService _registerUserService;
        public IRegisterUserService registerUserService
        {
            get
            {
                return _registerUserService = _registerUserService ?? new RegisterUserService(_context);
            }
        }
        private IGetRolesService _getRolesService;
        public IGetRolesService getRolesService
        {
            get
            {
                return _getRolesService = _getRolesService ?? new GetRolesService(_context);
            }
        }

        private IGetUserService _getUserService;
        public IGetUserService getUserService
        {
            get
            {
                return _getUserService = _getUserService ?? new GetUserService(_context);
            }
        }
        private IGetUserDetailService _getUserDetailService;
        public IGetUserDetailService getUserDetailService
        {
            get
            {
                return _getUserDetailService = _getUserDetailService ?? new GetUserDetailService(_context);
            }
        }
    }
}
