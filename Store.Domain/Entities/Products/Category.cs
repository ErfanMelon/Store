using Store.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Products
{
    public class Category:BaseEntity
    {
        [Key]
        public long CategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string CategoryTitle { get; set; }

        public virtual Category ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
