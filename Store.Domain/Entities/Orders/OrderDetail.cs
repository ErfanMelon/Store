using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        public long OrderDetailId { get; set; }
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public short Count { get; set; }
        /// <summary>
        /// Amount Per Product (ProductPrice)
        /// </summary>
        public int Amount { get; set; }
        public OrderState ProductState { get; set; }
        public int ProductRefund { get; set; } // if customer cancells product , amount of refund calculate
        public DateTime? DeliveredDate { get; set; }
    }
}
