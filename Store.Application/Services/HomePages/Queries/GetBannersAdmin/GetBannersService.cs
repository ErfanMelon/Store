using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Queries.GetBanners
{
    public class GetBannersService:IGetBannersService
    {
        private readonly IDataBaseContext _context;
        public GetBannersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetBanners>> Execute()
        {
            return new ResultDto<List<ResultGetBanners>>
            {
                Data=_context.Banners.Select(b=>new ResultGetBanners 
                {
                    ImageSrc=b.ImageSrc,
                    Link= b.Link,
                    Id=b.BannerId,
                    BannerLocation=b.BannerLocation,
                    Display=b.DisplayOnPage
                }).ToList(),
                IsSuccess=true,
            };
        }
    }
}
