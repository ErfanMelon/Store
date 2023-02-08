using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersAdmin;
public class BannerDto
{
    public int Id { get; set; }
    public string ImageSrc { get; set; }
    public string Link { get; set; }
    public bool Display { get; set; }
    public BannerLocation BannerLocation { get; set; }
}
public class GetBannersAdminQuery : IRequest<ResultDto<PaginationDto<BannerDto>>>
{
    public GetBannersAdminQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; }
    public int PageSize { get; }
    public class Handler : IRequestHandler<GetBannersAdminQuery, ResultDto<PaginationDto<BannerDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public Task<ResultDto<PaginationDto<BannerDto>>> Handle(GetBannersAdminQuery request, CancellationToken cancellationToken)
        {
            var banners = _context.Banners
                .AsNoTracking()
                .Select(b => new BannerDto
                {
                    ImageSrc = b.ImageSrc,
                    Link = b.Link ?? "",
                    Id = b.BannerId,
                    BannerLocation = b.BannerLocation,
                    Display = b.DisplayOnPage
                }).ToPaged(request.Page, request.PageSize);
            if (banners.Items.Any())
                return Task.FromResult(new ResultDto<PaginationDto<BannerDto>>(banners));
            return Task.FromResult(new ResultDto<PaginationDto<BannerDto>>("هیچ بنری موجود نیست"));
        }
    }
}