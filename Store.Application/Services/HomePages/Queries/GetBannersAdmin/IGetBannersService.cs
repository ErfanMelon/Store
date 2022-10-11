using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBanners
{
    public interface IGetBannersService
    {
        ResultDto<List<ResultGetBanners>> Execute();
    }
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
    public class ResultGetBanners 
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public bool Display { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
