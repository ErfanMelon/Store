using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite
{
    public interface IGetBannersSiteService
    {
        ResultDto<ILookup<BannerLocation, GetBannerSiteDto>> Execute();
    }
}
