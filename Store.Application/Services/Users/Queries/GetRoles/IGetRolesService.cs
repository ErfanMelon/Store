using Store.Common.Dto;

namespace Store.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<ResultGetRoleDto>> Execute();
    }
}
