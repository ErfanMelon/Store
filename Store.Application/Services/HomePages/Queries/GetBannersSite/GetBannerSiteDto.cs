using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
    public class GetBannerSiteDto
    {
        public string Link { get; set; }
        public string ImgSrc { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
