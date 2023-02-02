using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetBrands;
public class GetBrandsQuery : IRequest<ResultDto<List<BrandBreifDto>>>
{
    public class Handler : IRequestHandler<GetBrandsQuery, ResultDto<List<BrandBreifDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<List<BrandBreifDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _context.ProductBrands.Select(b => new BrandBreifDto
            {
                BrandId = b.BrandId,
                BrandName = b.Brand,
            }).ToListAsync();

            if (brands.Any())
                return new ResultDto<List<BrandBreifDto>>(brands, true);

            return new ResultDto<List<BrandBreifDto>>("برندی موجود نیست");
        }
    }
}
public class BrandBreifDto
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}
