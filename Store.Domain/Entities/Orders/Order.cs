using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public long OrderId { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual RequestPay RequestPay { get; set; }
        public Guid PayId { get; set; }
        public int OrderRefund { get; set; }// if customer cancells some products , the Amount of refund calculate based orderdetail (default=0) 
        public OrderState OrderState { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
