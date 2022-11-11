using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// محصول
    /// </summary>
    public class Product:BaseEntity,IComparable
    {
        public long ProductId { get; set; }
        /// <summary>
        /// نام محصول
        /// </summary>
        public string ProductTitle { get; set; }
        /// <summary>
        /// توضیخات محصول
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// قیمت محصول
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// تعداد باقی مانده در انبار
        /// </summary>
        public int Inventory { get; set; }
        /// <summary>
        /// درسایت نمایش داده شود
        /// </summary>
        public bool Displayed { get; set; }
        /// <summary>
        /// تعداد بازدید
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// دسته بندی محصول
        /// </summary>
        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        /// <summary>
        /// تصاویر محصول
        /// </summary>
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        /// <summary>
        /// قابلیت های محصول
        /// </summary>
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
        /// <summary>
        /// برند محصول
        /// </summary>
        public virtual ProductBrand Brand { get; set; }
        public int BrandId { get; set; }
        /// <summary>
        /// نظرات محصول
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        /// <summary>
        /// این متد برای مقایسه دو کالا کاربرد دارد
        /// </summary>
        /// <param name="obj">کالا</param>
        /// <returns>بر اساس محبوبیت مقایسه میشود</returns>
        public int CompareTo(object? obj)
        {
            Product p = (Product)obj;
            int firstproductlikes = p.Comments.Any() ? (int)p.Comments.Average(l => l.Score):0;
            int otherproductlikes = this.Comments.Any() ? (int)this.Comments.Average(l => l.Score):0;
            if (firstproductlikes>otherproductlikes)
            {
                return 1;
            }
            if (firstproductlikes<otherproductlikes)
            {
                return -1;
            }
            return 0;
        }
    }
}
