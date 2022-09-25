using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Users
{
    public class Role:BaseEntity
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
