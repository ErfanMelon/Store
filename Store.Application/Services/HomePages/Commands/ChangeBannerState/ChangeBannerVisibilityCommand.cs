using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.ChangeBannerState;
public class ChangeBannerVisibilityCommand : IRequest<ResultDto>
{
    public int BannerId { get; set; }
    public class Handler : IRequestHandler<ChangeBannerVisibilityCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(ChangeBannerVisibilityCommand request, CancellationToken cancellationToken)
        {
            var banner = await _context.Banners.FindAsync(request.BannerId);
            if (banner is null)
                throw new ArgumentNullException("بنری یافت نشد");

            banner.DisplayOnPage = !banner.DisplayOnPage;
            await _context.SaveChangesAsync(cancellationToken);

            string state = banner.DisplayOnPage ? "داده میشود" : "داده نمیشود";
            return new ResultDto(true, $"بنر در سایت نمایش {state}");
        }
    }
}
