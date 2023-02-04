using FluentValidation;
using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddCategory;

public class AddCategoryCommand : IRequest<ResultDto>
{
    public string CategoryTitle { get; set; }
    public long? ParentCategoryId { get; set; }
    public AddCategoryCommand(string categoryTitle,long? parentCategoryId=null)
    {
        CategoryTitle = categoryTitle;
        ParentCategoryId = parentCategoryId;
    }
    public AddCategoryCommand()
    {

    }
    public class Validator : AbstractValidator<AddCategoryCommand>
    {
        public Validator()
        {
            RuleFor(e => e.CategoryTitle).NotEmpty().WithMessage("نام دسته بندی معتبر نیست");
            RuleFor(e => e.CategoryTitle).MaximumLength(200).WithMessage("عنوان دسته بندی نباید از 200 کاراکتر بیشتر باشد !");
        }
    }
    public class Handler : IRequestHandler<AddCategoryCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var parentCategory = await _context.Categories.FindAsync(request.ParentCategoryId);
            if (parentCategory != null && parentCategory.ParentCategoryId.HasValue)
                throw new NotSupportedException("در حال حاظر امکان دسته بندی تودرتو بیشتر از 1 امکان پذیر نمیباشد");
            
            var newCategory = new Category
            {
                CategoryTitle = request.CategoryTitle,
                ParentCategory = parentCategory,
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $"{request.CategoryTitle} با موفقیت اضافه شد!");

        }
    }
}
