using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
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
}
