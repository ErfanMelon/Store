using Store.Domain.Entities.Common;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Products
{
    /// <summary>
    /// نظرات کاربران
    /// </summary>
    public class Comment:BaseEntity
    {
        public long CommentId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public virtual User User { get; set; }
        public long UserId { get; set; }
        /// <summary>
        /// محصول
        /// </summary>
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        /// <summary>
        /// امتیاز به محصول که از 0 تا 5 است
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// عنوان نظر
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نظر
        /// </summary>
        public string UserComment { get; set; }
        /// <summary>
        /// نقاط قوت
        /// </summary>
        public string Pros { get; set; }
        /// <summary>
        /// نقاط ضعف
        /// </summary>
        public string Cons { get; set; }

    }
}
