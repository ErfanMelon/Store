using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.DeleteBanner;
public class DeleteBannerCommand : IRequest<ResultDto>
{
    public int BannerId { get; set; }
    public class Handler : IRequestHandler<DeleteBannerCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _context.Banners.FindAsync(request.BannerId);
            if (banner is null)
                throw new ArgumentNullException("بنر معتبر نیست");

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, "با موفقیت حذف شد !");
        }
    }
}