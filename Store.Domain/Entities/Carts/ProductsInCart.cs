using Store.Domain.Entities.Common;
using Store.Domain.Entities.Products;

namespace Store.Domain.Entities.Carts
{
    /// <summary>
    /// محصول در سبد خرید
    /// </summary>
    public class ProductsInCart:BaseEntity
    {
        public long Id { get; set; }
        /// <summary>
        /// محصول انتخابی
        /// </summary>
        public virtual Product SelectedProduct { get; set; }
        public long ProductId { get; set; }
        /// <summary>
        /// سبد خرید کاربر
        /// </summary>
        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }
        /// <summary>
        /// تعداد کالای انتخاب شده
        /// </summary>
        public short ProductCount { get; set; }
    }
}
