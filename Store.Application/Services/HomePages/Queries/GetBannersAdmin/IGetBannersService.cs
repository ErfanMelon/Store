﻿using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Queries.GetBanners
{
    public interface IGetBannersService
    {
        ResultDto<ResultGetBanners> Execute(int page,int pagesize);
    }
}
