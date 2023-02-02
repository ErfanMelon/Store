using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteBrandService;
public class DeleteBrandCommand : IRequest<ResultDto>
{
    public int BrandId { get; set; }
    public DeleteBrandCommand(int brandId)
    {
        BrandId = brandId;
    }
    public DeleteBrandCommand()
    {

    }
    public class Validator : AbstractValidator<DeleteBrandCommand>
    {
        public Validator()
        {
            RuleFor(e => e.BrandId).GreaterThan(0).WithMessage("شناسه برند معتبر نیست");
        }
    }
    public class Handler : IRequestHandler<DeleteBrandCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _context.ProductBrands
                .AsNoTracking()
                .SingleOrDefaultAsync(e=>e.BrandId==request.BrandId);

            if (brand is null)
                throw new ArgumentNullException("برند پیدا نشد");

            _context.ProductBrands.Remove(brand);

            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto(true, $"{brand.Brand} با موفقیت حذف شد !");
        }
    }
}
