using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    public class ProductImages:BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public string Src { get; set; }
    }
}
