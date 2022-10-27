using Microsoft.AspNetCore.Hosting;
using Store.Application.Services.Common.Commands.UploadFile;

namespace Store.Application.Services.Common.Commands.DeleteFile
{
    public class DeleteFileService : IDeleteFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public DeleteFileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public UploadDto Execute(string filepath)
        {
            if (filepath != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, filepath);

                if (!File.Exists(filePath))
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }


                File.Delete(filePath);

                return new UploadDto()
                {
                    FileNameAddress = folder + filepath,
                    Status = true,
                };
            }
            return null;
        }
    }
}
