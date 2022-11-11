using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Users
{
    /// <summary>
    /// سطح دسترسی کاربران
    /// </summary>
    public class Role:BaseEntity
    {
        public int RoleId { get; set; }
        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// افراد دارای مجوز دسترسی
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
