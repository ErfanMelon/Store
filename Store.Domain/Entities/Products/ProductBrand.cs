using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Products
{
    public class ProductBrand:BaseEntity
    {
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
