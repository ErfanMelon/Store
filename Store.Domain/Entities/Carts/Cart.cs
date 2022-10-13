using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Carts
{
    public class Cart:BaseEntity
    {
        public long CartId { get; set; }
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool CurrentCart { get; set; } // if user paid Cart sets false
        public ICollection<ProductsInCart> ItemsInCart { get; set; }
    }
}
