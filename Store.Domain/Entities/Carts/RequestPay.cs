using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Carts
{
    /// <summary>
    /// درخواست برای پرداخت سفارش
    /// </summary>
    public class RequestPay:BaseEntity
    {
        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        public Guid PayId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        public long UserId { get; set; }
        /// <summary>
        /// سبد خرید
        /// </summary>
        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }
        /// <summary>
        /// مجموع هزینه
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// آیا پرداخت صورت گرفته
        /// </summary>
        public bool IsPay { get; set; }
        /// <summary>
        /// درصورت پرداخت شدن تاریخ پرداخت
        /// </summary>
        public DateTime? PayDate { get; set; }
        /// <summary>
        /// مقادیر دریافتی از بانک
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// مقادیر دریافتی از بانک
        /// </summary>
        public long RefId { get; set; } = 0;

    }
}
