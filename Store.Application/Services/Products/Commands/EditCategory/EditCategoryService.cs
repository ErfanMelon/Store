using Store.Application.Interfaces.Context;
using Store.Application.Validations.Product;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.EditCategory
{
    public class EditCategoryService : IEditCategoryService
    {
        private readonly IDataBaseContext _context;
        public EditCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(EditCategoryDto request)
        {
            var validationrules = new EditCategoryValidation();
            var validate = validationrules.Validate(request);
            if (!validate.IsValid)
            {
                return new ResultDto { Message = validate.Errors[0].ErrorMessage };
            }
            try
            {
                var category = _context.Categories.Find(request.CategoryId);

                if (category==null)
                {
                    return new ResultDto { Message = "داده ای پیدا نشد !" };
                }
                // Update Category
                category.CategoryTitle = request.CategoryTitle;

                var parentCategory = _context.Categories.Find(request.ParentCategoryId);
                if (parentCategory!=null && parentCategory.ParentCategoryId != null)
                    return new ResultDto {Message = "در حال حاظر امکان دسته بندی تودرتو بیشتر از 1 امکان پذیر نمیباشد !" };
                
                category.ParentCategory = parentCategory;
                category.UpdateTime = DateTime.Now;
                
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
