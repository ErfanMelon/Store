using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.HomePages
{
    /// <summary>
    /// تصاویر نمایشی در سایت
    /// </summary>
    public class Banner:BaseEntity
    {
        public int BannerId { get; set; }
        /// <summary>
        /// تصویر بنر یا اسلایدر
        /// </summary>
        public string ImageSrc { get; set; }
        /// <summary>
        /// لینک بنر 
        /// </summary>
        public string? Link { get; set; }
        /// <summary>
        /// موقعیت قرار گیری در سایت
        /// </summary>
        public BannerLocation BannerLocation { get; set; }
        /// <summary>
        /// آیا درصفحه اصلی نمایش داده شود
        /// </summary>
        public bool DisplayOnPage { get; set; } = true;
    }
    public enum BannerLocation
    {
        /// <summary>
        /// Main Banners اسلایدر
        /// </summary>
        Slider=0,
        /// <summary>
        /// First Left Banner
        /// </summary>
        L1 = 1,
        /// <summary>
        /// Second Left Banner
        /// </summary>
        L2 = 2,
        /// <summary>
        /// Right Banner
        /// </summary>
        R1 = 3,
        /// <summary>
        /// Wide Banner
        /// </summary>
        CenterFullScreen = 4,
        /// <summary>
        /// Mini Slider 1
        /// </summary>
        G1 = 5,
        /// <summary>
        /// Mini Slider 2
        /// </summary>
        G2 = 6,
    }
}
