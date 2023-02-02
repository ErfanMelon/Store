using FluentValidation;
using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddBrand;

public class AddBrandCommand : IRequest<ResultDto>
{
    public string BrandName { get; set; }

    public AddBrandCommand(string brandName)
    {
        BrandName = brandName;
    }
    public AddBrandCommand()
    {

    }
    public class Validator : AbstractValidator<AddBrandCommand>
    {
        public Validator()
        {
            RuleFor(e => e.BrandName).NotEmpty().WithMessage("نام برند صحیح نمیباشد");
        }
    }
    public class Handler : IRequestHandler<AddBrandCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public Task<ResultDto> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            _context.ProductBrands.Add(new ProductBrand { Brand = request.BrandName });

            _context.SaveChangesAsync(cancellationToken);

            return Task.FromResult(new ResultDto(true, $"{request.BrandName} با موفقیت ثبت شد !"));
        }
    }
}