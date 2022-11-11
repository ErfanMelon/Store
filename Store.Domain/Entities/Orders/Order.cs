using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Orders
{
    /// <summary>
    /// سفارش کاربر
    /// </summary>
    public class Order : BaseEntity
    {
        public long OrderId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        public long UserId { get; set; }
        /// <summary>
        /// درخواست پرداخت
        /// </summary>
        public virtual RequestPay RequestPay { get; set; }
        public Guid PayId { get; set; }
        /// <summary>
        /// مبلغ عودت درصورتی که سفارش لغو شود
        /// </summary>
        public int OrderRefund { get; set; }
        /// <summary>
        /// سفارش در چه مرحله ای از تحویل است
        /// </summary>
        public OrderState OrderState { get; set; }
        /// <summary>
        /// اطلاعات هر محصول
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; }
        /// <summary>
        /// تاریخ تحویل سفارش
        /// </summary>
        public DateTime? DeliveredDate { get; set; }
        /// <summary>
        /// آدرس تحویل
        /// </summary>
        public string UserAddress { get; set; }
        /// <summary>
        /// کدپستی تحویل
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// توضیحات بیشتر درمورد سفارش
        /// </summary>
        public string? Description { get; set; }

    }
}
