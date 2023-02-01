using MediatR;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Validations.HomePage;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;
using static Store.Common.UploadPath;

namespace Store.Application.Services.HomePages.Commands.AddBanner
{
    public class AddBannerService : IAddBannerService
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;
        public AddBannerService(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public ResultDto Execute(RequestBannerDto request)
        {
            BannerValidation validations = new BannerValidation();
            var validate = validations.Validate(request);
            if (!validate.IsValid)
            {
                return new ResultDto { Message = validate.Errors[0].ErrorMessage };
            }
            var imgresult = _mediator.Send(new UploadFileCommand(request.Image, UploadFileType.BannerImage)).Result;
            if (imgresult.Status)
            {
                var banner = new Banner
                {
                    ImageSrc = imgresult.FileNameAddress,
                    Link = request.Link,
                    BannerLocation = request.BannerLocation,
                    DisplayOnPage = request.DisplayOnSite
                };
                _context.Banners.Add(banner);
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true };

            }
            else
            {
                return new ResultDtoError();
            }
        }
    }
}
