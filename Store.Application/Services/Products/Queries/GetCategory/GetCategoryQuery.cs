using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategory;
public class GetCategoryQuery:IRequest<ResultDto<CategoryDto>>
{
    public long CategoryId { get; set; }

    public GetCategoryQuery(long categoryId)
    {
        CategoryId = categoryId;
    }
    public GetCategoryQuery()
    {

    }
    public class Validator:AbstractValidator<GetCategoryQuery>
    {
        public Validator()
        {
            RuleFor(e => e.CategoryId).GreaterThan(0).WithMessage("دسته بندی معتبر نیست");
        }
    }
    public class Handler:IRequestHandler<GetCategoryQuery, ResultDto<CategoryDto>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {

            var category =await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.CategoryId == request.CategoryId);

            if (category is null)
                throw new ArgumentNullException("دسته بندی پیدا نشد");

            CategoryDto result = new CategoryDto
                (category.CategoryId,
                category.CategoryTitle,
                category.ParentCategoryId.HasValue ? // if parentcategory has value convert it to CategoryDto otherwise null
                new CategoryDto(category.ParentCategoryId.Value, category.ParentCategory.CategoryTitle)
                : null,
                category.SubCategories.Any() ? // if category has any subs , return List<CategoryDto> otherwise null
                category.SubCategories.Select(e=>new CategoryDto(e.CategoryId,e.CategoryTitle)).ToList()
                :null);

            return new ResultDto<CategoryDto>(result);
        }
    }
}



