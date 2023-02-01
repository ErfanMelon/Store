using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Common;

namespace Store.Application.Services.Common.Commands.UploadFile;

public class UploadFileCommand : IRequest<UploadDto>
{
    public IFormFile File { get; }
    public UploadPath.UploadFileType TypeOfFile { get; }
    public UploadFileCommand(IFormFile file, UploadPath.UploadFileType fileType)
    {
        File = file;
        TypeOfFile = fileType;
    }

    public class Handler : IRequestHandler<UploadFileCommand, UploadDto>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Handler(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Task<UploadDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            string folder = UploadPath.Get(request.TypeOfFile);

            var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            string fileName = DateTime.Now.Ticks + request.File.FileName;
            var filePath = Path.Combine(uploadsRootFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                request.File.CopyTo(fileStream);
            }

            return Task.FromResult(new UploadDto
            {
                FileNameAddress = folder + fileName,
                Status = true,
            });
        }
    }
    public class Validator : AbstractValidator<UploadFileCommand>
    {
        public Validator()
        {
            RuleFor(e => e.File).NotEmpty();
            RuleFor(e => e.File.Length).NotEqual(0);
        }
    }
}