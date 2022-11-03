using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Users
{
    public class Role:BaseEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
