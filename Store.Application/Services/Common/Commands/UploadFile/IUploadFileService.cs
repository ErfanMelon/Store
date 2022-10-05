using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Store.Application.Services.Common.Commands.UploadFile
{
    public interface IUploadFileService
    {
        UploadDto Execute(IFormFile file, UploadFileType fileType);
    }
    public class UploadFileService : IUploadFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadFileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public UploadDto Execute(IFormFile file, UploadFileType fileType)
        {
            if (file != null)
            {
                string folder;
                switch (fileType)
                {
                    case UploadFileType.ProductImage:
                        folder = $@"images\ProductImages\";
                        break;
                    case UploadFileType.BannerImage:
                        folder = $@"images\BannerImages\";
                        break;
                    default:
                        folder = $@"images\";
                        break;
                }

                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }
    public class UploadDto
    {
        public string FileNameAddress { get; set; }
        public bool Status { get; set; }
    }
    public enum UploadFileType
    {
        ProductImage,
        BannerImage
    }
}
