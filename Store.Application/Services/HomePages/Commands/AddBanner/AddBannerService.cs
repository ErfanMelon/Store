using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.UploadFile;
using Store.Application.Validations.HomePage;
using Store.Common.Dto;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Commands.AddBanner
{
    public class AddBannerService : IAddBannerService
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;
        public AddBannerService(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public ResultDto Execute(RequestBannerDto request)
        {
            BannerValidation validations = new BannerValidation();
            var validate = validations.Validate(request);
            if (!validate.IsValid)
            {
                return new ResultDto { Message = validate.Errors[0].ErrorMessage };
            }
            var imgresult = _uploadFileService.Execute(request.Image,UploadFileType.BannerImage);
            if (imgresult.Status)
            {
                var banner = new Banner
                {
                    ImageSrc = imgresult.FileNameAddress,
                    Link = request.Link,
                    BannerLocation=request.BannerLocation,
                    DisplayOnPage=request.DisplayOnSite
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
