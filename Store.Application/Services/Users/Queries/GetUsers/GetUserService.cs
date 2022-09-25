using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUserService : IGetUserService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public GetUserService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _dataBaseContext.Users
                .Include(u=>u.UserInRoles)
                .ThenInclude(uir=>uir.Role)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(u =>
                u.Email.ToLower().Contains(request.SearchKey) ||
                u.UserFullName.ToLower().Contains(request.SearchKey));
            }
            var usersList = users.ToPaged(request.Page, request.PageSize, out int rows).Select(u => new GetUserDto
            {
                Email = u.Email,
                IsActive = u.IsActive,
                UserFullName = u.UserFullName,
                UserId = u.UserId,
                UserRoleId = u.UserInRoles.First().RoleId
            }).ToList();
            return new ResultGetUserDto
            {
                RowsCount = rows,
                CurrentPage=request.Page,
                PageSize=request.PageSize,
                Users = usersList,
            };
        }
    }
}
