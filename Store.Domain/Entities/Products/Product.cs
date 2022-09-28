using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    public class Product : BaseEntity, IComparable
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

        public int CompareTo(object? obj)
        {
            if (obj == null) return 0;
            Product otherProduct = obj as Product;
            if (otherProduct != null)
            {
                int FirObj = this.ProductLikes.Any() ? (int)this.ProductLikes.Average(l => l.Score) : 0;
                int SecObj = otherProduct.ProductLikes.Any() ? (int)otherProduct.ProductLikes.Average(l => l.Score) : 0;
                return FirObj.CompareTo(SecObj);
            }
            else
                throw new ArgumentException("OBJ Not Product");
        }
    }
}
