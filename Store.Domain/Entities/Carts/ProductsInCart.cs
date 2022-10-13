using Store.Domain.Entities.Common;
using Store.Domain.Entities.Products;

namespace Store.Domain.Entities.Carts
{
    public class ProductsInCart:BaseEntity
    {
        public long Id { get; set; }
        public virtual Product SelectedProduct { get; set; }
        public long ProductId { get; set; }
        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }
        public short ProductCount { get; set; }
    }
}
