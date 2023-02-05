using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductForEdit;
public class GetProductForEditQuery : IRequest<ResultDto<EditProductCommand>>
{
    public GetProductForEditQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
    public class Handler : IRequestHandler<GetProductForEditQuery, ResultDto<EditProductCommand>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<EditProductCommand>> Handle(GetProductForEditQuery request, CancellationToken cancellationToken)
        {

            var product = await _context.Products
                .Include(p => p.ProductFeatures)
                .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (product is null)
                throw new ArgumentNullException("محصول معتبر نیست");

            EditProductCommand editProduct = new EditProductCommand
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Displayed = product.Displayed,
                Inventory = product.Inventory,
                Price = product.Price,
                ProductId = product.ProductId,
                ProductTitle = product.ProductTitle,
                ProductFeatures = product
                .ProductFeatures
                .Select(e => new RequestFeatureDto
                {
                    Feature = e.Feature,
                    Value = e.FeatureValue
                }).ToList()
            };
            return new ResultDto<EditProductCommand>(editProduct);
        }
    }
}