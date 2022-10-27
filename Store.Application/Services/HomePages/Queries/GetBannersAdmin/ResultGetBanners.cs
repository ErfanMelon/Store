using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBanners
{
    public class ResultGetBanners 
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public bool Display { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
