using Store.Application.Interfaces.Context;
using Store.Common;
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

        public ResultDto<ResultGetBanners> Execute(int page, int pagesize)
        {
            var banners = _context.Banners.Select(b => new BannerDto
            {
                ImageSrc = b.ImageSrc,
                Link = b.Link,
                Id = b.BannerId,
                BannerLocation = b.BannerLocation,
                Display = b.DisplayOnPage
            }).ToPaged(page,pagesize,out int rowcounts).ToList();
            return new ResultDto<ResultGetBanners>
            {
                Data=new ResultGetBanners
                {
                    Banners=banners,
                    CurrentPage=page,
                    PageSize=pagesize,
                    RowsCount= rowcounts
                },
                IsSuccess=true,
            };
        }
    }
}
