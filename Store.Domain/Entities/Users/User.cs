using Store.Domain.Entities.Common;
using Store.Domain.Entities.Orders;

namespace Store.Domain.Entities.Users
{
    /// <summary>
    /// کاربر
    /// </summary>
    public class User : BaseEntity
    {
        public long UserId { get; set; }
        /// <summary>
        /// نام و نام خانوادگی کاربر
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// ایمیل کاربر
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// کلمه عبور
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// وضعیت کاربر برای خرید
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// سطح دسترسی کاربر
        /// </summary>
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        /// <summary>
        /// آدرس کاربر
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// سفارشات کاربر
        /// </summary>
        public ICollection<Order> Orders { get; set; }
        /// <summary>
        /// شماره تلفن کاربر
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// کدپستی کاربر
        /// </summary>
        public string? ZipCode { get; set; }

    }
}
