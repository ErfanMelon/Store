using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategories;
public class GetCategoriesQuery : IRequest<ResultDto<List<CategoryBriefDto>>>
{
    public class Handler : IRequestHandler<GetCategoriesQuery, ResultDto<List<CategoryBriefDto>>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<List<CategoryBriefDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                       .Include(p => p.ParentCategory)
                       .Include(p => p.SubCategories)
                       .AsNoTracking()
                       .AsQueryable();
            var categories = await query.Select(p => new CategoryBriefDto
            {
                CategoryId = p.CategoryId,
                CategoryTitle = p.CategoryTitle,
                Parent = p.ParentCategoryId.HasValue ? new CategoryBriefDto
                {
                    CategoryId = p.ParentCategory.CategoryId,
                    CategoryTitle = p.ParentCategory.CategoryTitle,
                    HasChild = true
                } : null,

                HasChild = p.SubCategories.Any(),
            }).ToListAsync();
            return new ResultDto<List<CategoryBriefDto>>(categories);
        }
    }
}

