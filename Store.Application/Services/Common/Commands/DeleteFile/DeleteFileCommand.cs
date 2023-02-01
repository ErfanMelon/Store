using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Store.Application.Services.Common.Commands.DeleteFile;

public class DeleteFileCommand : IRequest
{
    public string FilePath { get; }
    public DeleteFileCommand(string filePath)
    {
        FilePath = filePath;
    }

    public class Handler : IRequestHandler<DeleteFileCommand>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Handler(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, request.FilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.FromResult(Unit.Value);
        } 
    }
    public class Validator : AbstractValidator<DeleteFileCommand>
        {
            public Validator()
            {
                RuleFor(e => e.FilePath).NotEmpty();
            }
        }
}

