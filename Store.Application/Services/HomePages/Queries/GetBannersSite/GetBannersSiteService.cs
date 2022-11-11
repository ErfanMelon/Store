using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
    public class GetBannersSiteService: IGetBannersSiteService
    {
        private readonly IDataBaseContext _context;
        public GetBannersSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ILookup<BannerLocation,GetBannerSiteDto>> Execute()
        {
            var result = _context.Banners
                .Where(b=>b.DisplayOnPage==true)
                .Select(b => new GetBannerSiteDto 
            {
                ImgSrc=b.ImageSrc,
                Link=b.Link,
                BannerLocation=b.BannerLocation
            }).ToLookup(b=>b.BannerLocation);
            return new ResultDto<ILookup<BannerLocation, GetBannerSiteDto>> { Data = result, IsSuccess = true };
        }
    }
}
