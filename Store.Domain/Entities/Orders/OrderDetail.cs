using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Orders
{
    /// <summary>
    /// اطلاعات کالا در سفارش
    /// </summary>
    public class OrderDetail : BaseEntity
    {
        public long OrderDetailId { get; set; }
        /// <summary>
        /// سفارش
        /// </summary>
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }
        /// <summary>
        /// ایدی محصول خریداری شده
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// نام محصول خریداری شده
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// تعداد محصول سفارش داده شده
        /// </summary>
        public short Count { get; set; }
        /// <summary>
        /// مبلغ به ازای هر محصول
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// سفارش در چه مرحله ای از تحویل است
        /// </summary>
        public OrderState ProductState { get; set; }
        /// <summary>
        /// مبلغ عودت درصورتی که سفارش لغو شود
        /// </summary>
        public int ProductRefund { get; set; }
        /// <summary>
        /// تاریخ تحویل سفارش
        /// </summary>
        public DateTime? DeliveredDate { get; set; }
        /// <summary>
        /// توضیحات بیشتر درمورد سفارش
        /// </summary>
        public string? MoreDetail { get; set; }
    }
}
