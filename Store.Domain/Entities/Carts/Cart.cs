using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Carts
{
    /// <summary>
    /// محل نگه داری کالاهای کاربر تا زمان خرید
    /// </summary>
    public class Cart:BaseEntity
    {
        public long CartId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        /// <summary>
        /// ایدی مرورگر
        /// </summary>
        public Guid BrowserId { get; set; }
        /// <summary>
        /// ایا این سبدخرید پرداخت شده است
        /// </summary>
        public bool CurrentCart { get; set; }
        /// <summary>
        /// کالا های این سبدخرید
        /// </summary>
        public ICollection<ProductsInCart> ItemsInCart { get; set; }
    }
}
