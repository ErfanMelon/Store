using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// برند محصولات
    /// </summary>
    public class ProductBrand:BaseEntity
    {
        public int BrandId { get; set; }
        /// <summary>
        /// نام برند
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// محصولاتی که از این برند هستند
        /// </summary>
        public virtual List<Product> Products { get; set; }
    }
}
