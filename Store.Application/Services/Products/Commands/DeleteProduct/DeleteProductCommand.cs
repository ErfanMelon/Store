using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Common.Commands.DeleteFile;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.DeleteProduct;
public class DeleteProductCommand : IRequest<ResultDto>
{
    public long ProductId { get; set; }
    public class Handler : IRequestHandler<DeleteProductCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;

        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                            .Include(p => p.ProductImages)
                            .Include(p => p.ProductFeatures)
                            .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (product is null)
                throw new ArgumentNullException("محصول معتبر نیست");
            foreach (ProductImages image in product.ProductImages)
            {
                await _mediator.Send(new DeleteFileCommand(image.Src));
            }
            _context.Products.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $" {product.ProductTitle} با موفقیت حذف شد !");
        }
    }
}

