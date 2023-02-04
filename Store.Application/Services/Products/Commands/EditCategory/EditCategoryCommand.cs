using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.EditCategory;

public class EditCategoryCommand : IRequest<ResultDto>
{
    public long CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public long? ParentCategoryId { get; set; }

    public EditCategoryCommand(long categoryId, string categoryTitle, long? parentCategoryId)
    {
        CategoryId = categoryId;
        CategoryTitle = categoryTitle;
        ParentCategoryId = parentCategoryId;
    }
    public EditCategoryCommand()
    {

    }
    public class Validator : AbstractValidator<EditCategoryCommand>
    {
        public Validator()
        {
            RuleFor(e => e.CategoryTitle).NotEmpty().WithMessage("لطفا عنوان دسته بندی را وارد کنید !");
            RuleFor(e => e.CategoryTitle).MaximumLength(200).WithMessage("عنوان دسته بندی نباید از 200 کاراکتر بیشتر باشد !");
            RuleFor(e => e.CategoryId).NotEqual(e => e.ParentCategoryId.Value).When(e => e.ParentCategoryId.HasValue).WithMessage("دسته بندی نمیتواند زیرمجموعه خودش باشد !");
            RuleFor(e => e.CategoryId).GreaterThan(0).WithMessage("دسته بندی معتبر نیست");
        }
    }
    public class Handler : IRequestHandler<EditCategoryCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(e => e.ParentCategory)
                .Include(e=>e.SubCategories)
                .SingleOrDefaultAsync(e=>e.CategoryId== request.CategoryId);

            if (category is null)
                throw new ArgumentNullException("دسته بندی پیدا نشد");


            var parentCategory = await _context.Categories.FindAsync(request.ParentCategoryId);

            if ((parentCategory != null && parentCategory.ParentCategoryId.HasValue) || (category.SubCategories.Any() && parentCategory != null))
                throw new NotSupportedException("در حال حاظر امکان دسته بندی تودرتو بیشتر از 1 امکان پذیر نمیباشد");

            category.CategoryTitle = request.CategoryTitle;
            category.ParentCategory = parentCategory;

            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto(true, $"{request.CategoryTitle} با موفقیت ویرایش شد!");
        }
    }
}

