using Store.Application.Services.Common.Commands.UploadFile;

namespace Store.Application.Services.Common.Commands.DeleteFile
{
    public interface IDeleteFileService
    {
        UploadDto Execute(string filepath);
    }
}
