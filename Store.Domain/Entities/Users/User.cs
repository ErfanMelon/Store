using Store.Domain.Entities.Common;
using Store.Domain.Entities.Orders;

namespace Store.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ZipCode { get; set; }

    }
}
