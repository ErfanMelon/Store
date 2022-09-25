using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Users
{
    public class UserInRole:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
