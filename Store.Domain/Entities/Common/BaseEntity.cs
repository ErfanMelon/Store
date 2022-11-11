namespace Store.Domain.Entities.Common
{
    public class BaseEntity
    {
        /// <summary>
        /// تاریخ درج شدن داده
        /// </summary>
        public DateTime InsertTime { get; set; } = DateTime.Now;
        /// <summary>
        /// تاریخ ویرایش داده
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// آیا داده حذف شده است
        /// </summary>
        public bool IsRemoved { get; set; } = false;
        /// <summary>
        /// تاریخ حذف داده
        /// </summary>
        public DateTime? RemoveTime { get; set; }
    }
}
