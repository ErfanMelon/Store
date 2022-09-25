using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    public class ProductFeatures:BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public string Feature { get; set; }
        public string FeatureValue { get; set; }
    }
}
