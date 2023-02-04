using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<ResultDto>
{
    public long CategoryId { get; set; }

    public DeleteCategoryCommand(long categoryId)
    {
        CategoryId = categoryId;
    }
    public DeleteCategoryCommand()
    {

    }
    public class Validator : AbstractValidator<DeleteCategoryCommand>
    {
        public Validator()
        {
            RuleFor(e => e.CategoryId).GreaterThan(0).WithMessage("دسته بندی معتبر نیست");
        }
    }
    public class Handler : IRequestHandler<DeleteCategoryCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
               .SingleOrDefaultAsync(e => e.CategoryId == request.CategoryId);

            if (category is null)
                throw new ArgumentNullException("دسته بندی پیدا نشد");

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $"{category.CategoryTitle} با موفقیت حذف شد !");
        }
    }
}