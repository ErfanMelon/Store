using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
    public interface IGetBannersSiteService
    {
        ResultDto<List<GetBannerSiteDto>> Execute();
    }
}
