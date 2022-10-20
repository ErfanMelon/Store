using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Carts
{
    public class RequestPay:BaseEntity
    {
        public Guid PayId { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }
        public int Price { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

    }
}
