using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using static Store.Common.UploadPath;

namespace Store.Application.Services.HomePages.Commands.AddBanner;
public class AddBannerCommand : IRequest<ResultDto>
{
    public bool DisplayOnSite { get; set; }
    public IFormFile? Image { get; set; }
    public string? Link { get; set; }
    public BannerLocation BannerLocation { get; set; }
    public class Validator : AbstractValidator<AddBannerCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Image).NotEmpty().WithMessage("لطفا تصویر را وارد کنید !");
            RuleFor(e => e.BannerLocation).IsInEnum().WithErrorCode("موقعیت وارد شده صحیح نمیباشد !");
        }
    }
    public class Handler : IRequestHandler<AddBannerCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;
        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<ResultDto> Handle(AddBannerCommand request, CancellationToken cancellationToken)
        {
            var imgresult = await _mediator.Send(new UploadFileCommand(request.Image, UploadFileType.BannerImage));
            if (imgresult?.Status ?? false)
            {
                var banner = new Banner
                {
                    ImageSrc = imgresult.FileNameAddress,
                    Link = request.Link,
                    BannerLocation = request.BannerLocation,
                    DisplayOnPage = request.DisplayOnSite
                };
                await _context.Banners.AddAsync(banner);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResultDto(true, "بنر اضافه شد");

            }
            return new ResultDto("خطا رخ داد");
        }
    }
}