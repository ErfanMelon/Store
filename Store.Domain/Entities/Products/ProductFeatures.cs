using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// ویژگی محصول
    /// </summary>
    public class ProductFeatures:BaseEntity
    {
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        /// <summary>
        /// عنوان ویژگی
        /// </summary>
        public string Feature { get; set; }
        /// <summary>
        /// مقدار ویژگی
        /// </summary>
        public string FeatureValue { get; set; }
    }
}
