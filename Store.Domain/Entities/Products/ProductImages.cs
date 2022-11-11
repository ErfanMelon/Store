using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// تصویر محصول
    /// </summary>
    public class ProductImages:BaseEntity
    {
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        /// <summary>
        /// آدرس تصویر ذخیره شده در سرور
        /// </summary>
        public string Src { get; set; }
    }
}
