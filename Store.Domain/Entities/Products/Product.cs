using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        [StringLength(200)]
        public string ProductTitle { get; set; }
        [Required]
        [StringLength(200)]
        public string Brand { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Inventory { get; set; }
        [Required]
        public bool Displayed { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
    }
}
