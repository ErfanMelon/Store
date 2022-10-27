using Store.Domain.Entities.Common;
using Store.Domain.Entities.Orders;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Users
{
    public class User:BaseEntity
    {
        [Key]
        public long UserId { get; set; }
        [Required]
        [StringLength(120)]
        public string UserFullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
