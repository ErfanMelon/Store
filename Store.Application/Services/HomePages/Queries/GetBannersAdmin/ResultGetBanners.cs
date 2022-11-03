using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBanners
{
    public class ResultGetBanners
    {
        public List<BannerDto> Banners { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
    }
    public class BannerDto
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public bool Display { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
