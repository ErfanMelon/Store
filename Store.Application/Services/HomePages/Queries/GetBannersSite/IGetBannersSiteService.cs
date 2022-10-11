using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
    public interface IGetBannersSiteService
    {
        ResultDto<List<GetBannerSiteDto>> Execute();
    }
    public class GetBannersSiteService: IGetBannersSiteService
    {
        private readonly IDataBaseContext _context;
        public GetBannersSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetBannerSiteDto>> Execute()
        {
            var result = _context.Banners
                .Where(b=>b.DisplayOnPage==true)
                .Select(b => new GetBannerSiteDto 
            {
                ImgSrc=b.ImageSrc,
                Link=b.Link,
                BannerLocation=b.BannerLocation
            }).ToList();
            return new ResultDto<List<GetBannerSiteDto>> { Data = result, IsSuccess = true };
        }
    }
    public class GetBannerSiteDto
    {
        public string Link { get; set; }
        public string ImgSrc { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
