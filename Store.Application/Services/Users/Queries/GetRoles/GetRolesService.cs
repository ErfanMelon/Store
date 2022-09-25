using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public GetRolesService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public ResultDto<List<ResultGetRoleDto>> Execute()
        {
            try
            {
                var roles = _dataBaseContext.Roles.ToList()
                .Select(r => new ResultGetRoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                }).ToList();
                return new ResultDto<List<ResultGetRoleDto>>
                {
                    Data = roles,
                    IsSuccess = true,
                    Message = "",
                };
            }
            catch (Exception)
            {
                return new ResultDto<List<ResultGetRoleDto>> { Message = "خطا" };
            }
        }
    }
}
