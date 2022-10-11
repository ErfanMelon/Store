using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.HomePages
{
    public class Banner:BaseEntity
    {
        public int BannerId { get; set; }
        public string ImageSrc { get; set; }
        public string? Link { get; set; }
        public BannerLocation BannerLocation { get; set; }
        public bool DisplayOnPage { get; set; } = true;
    }
    public enum BannerLocation
    {
        /// <summary>
        /// Main Banners
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
