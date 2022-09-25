using Store.Application.Interfaces.Context;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddCategory
{
    public class AddCategoryService : IAddCategoryService
    {
        private readonly IDataBaseContext _context;
        public AddCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(AddCategoryDto request)
        {
            var validator = new CategoryValidation();
            var validate = validator.Validate(request);
            if (!validate.IsValid)
            {
                return new ResultDto { IsSuccess = false, Message = validate.Errors[0].ErrorMessage };
            }

            try
            {
                var parentCategory = _context.Categories.Find(request.ParentCategoryId);
                if (parentCategory !=null && parentCategory.ParentCategoryId != null)
                    return new ResultDto {Message = "در حال حاظر امکان دسته بندی تودرتو بیشتر از 1 امکان پذیر نمیباشد !" };
                _context.Categories.Add(new Category
                {
                    CategoryTitle = request.CategoryTitle,
                    ParentCategory = parentCategory,
                });
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"{request.CategoryTitle} با موفقیت اضافه شد!" };
            }
            catch (Exception)
            {
                return new ResultDtoError();
            }
        }
    }
}
