using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Queries.GetBannersSite;
public class BannerSiteDto
{
    public string Link { get; set; }
    public string ImgSrc { get; set; }
}
public class GetBannerSiteQuery : IRequest<ResultDto<List<BannerSiteDto>>>
{
    public GetBannerSiteQuery(int count, BannerLocation position)
    {
        Count = count;
        Position = position;
    }

    public int Count { get; }
    public BannerLocation Position { get; }
    public class Validator : AbstractValidator<GetBannerSiteQuery>
    {
        public Validator()
        {
            RuleFor(e => e.Count).GreaterThan(0);
        }
    }
    public class Handler : IRequestHandler<GetBannerSiteQuery, ResultDto<List<BannerSiteDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<BannerSiteDto>>> Handle(GetBannerSiteQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Banners
                .AsNoTracking()
                .Where(b => b.BannerLocation == request.Position && b.DisplayOnPage)
                .Take(request.Count)
                 .Select(b => new BannerSiteDto
                 {
                     ImgSrc = b.ImageSrc,
                     Link = b.Link ?? "",
                 })
                 .ToListAsync(cancellationToken);
            return new ResultDto<List<BannerSiteDto>>(result);
        }
    }
}
