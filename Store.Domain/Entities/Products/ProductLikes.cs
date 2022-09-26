using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Products
{
    public class ProductLikes:BaseEntity
    {
        public long LikeId { get; set; }
        public int Score { get; set; } // 0-5 
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
