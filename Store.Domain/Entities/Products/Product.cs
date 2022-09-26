using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public int Views { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
        public virtual ProductBrand Brand { get; set; }
        public int BrandId { get; set; }
        public virtual ICollection<ProductLikes> ProductLikes { get; set; }
    }
}
