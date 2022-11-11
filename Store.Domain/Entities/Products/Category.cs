using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// دسته بندی برای محصولات
    /// </summary>
    public class Category:BaseEntity
    {
        public long CategoryId { get; set; }
        /// <summary>
        /// نام دسته بندی
        /// </summary>
        public string CategoryTitle { get; set; }
        /// <summary>
        /// دسته بندی والد درصورت وجود
        /// </summary>
        public virtual Category ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }
        /// <summary>
        /// دسته بندی های فرزند درصورت وجود
        /// </summary>

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
