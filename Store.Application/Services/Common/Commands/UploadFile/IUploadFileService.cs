using Microsoft.AspNetCore.Http;

namespace Store.Application.Services.Common.Commands.UploadFile
{
    public interface IUploadFileService
    {
        UploadDto Execute(IFormFile file, UploadFileType fileType);
    }
}
