using Store.Domain.Entities.Common;

namespace Store.Domain.Entities.HomePages
{
    public class Banner:BaseEntity
    {
        public int BannerId { get; set; }
        public string ImageSrc { get; set; }
        public string? Link { get; set; }
        public int Clicks { get; set; }
    }
}
