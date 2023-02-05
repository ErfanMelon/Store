using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetProductsAdmin;
public class GetProductsAdminQuery : IRequest<ResultDto<PaginationDto<ProductAdminDto>>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string SearchKey { get; set; }
    public GetProductsAdminQuery()
    {

    }

    public GetProductsAdminQuery(int page, int pageSize, string searchKey)
    {
        Page = page;
        PageSize = pageSize;
        SearchKey = searchKey;
    }

    public class Validator : AbstractValidator<GetProductsAdminQuery>
    {
        public Validator()
        {
            RuleFor(e => e.Page).GreaterThan(0);
            RuleFor(e => e.PageSize).GreaterThan(0);
        }
    }
    public class Handler : IRequestHandler<GetProductsAdminQuery, ResultDto<PaginationDto<ProductAdminDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<PaginationDto<ProductAdminDto>>> Handle(GetProductsAdminQuery request, CancellationToken cancellationToken)
        {
            var Result = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                Result = Result.Where(p =>
                p.Description.Contains(request.SearchKey) ||
                p.Brand.Brand.Contains(request.SearchKey) ||
                p.ProductTitle.Contains(request.SearchKey) ||
                p.Category.CategoryTitle.Contains(request.SearchKey)
                ).AsQueryable();
            }

            var ProductList = Result.Select(p => new ProductAdminDto
            {
                Category = p.Category.CategoryTitle,
                Price = p.Price,
                ProductTitle = p.ProductTitle,
                ProductId = p.ProductId
            }).ToPaged(request.Page, request.PageSize);

            return new ResultDto<PaginationDto<ProductAdminDto>>(ProductList);
        }
    }
}
