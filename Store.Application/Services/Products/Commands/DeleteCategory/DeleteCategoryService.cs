using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteCategory
{
    public class DeleteCategoryService : IDeleteCategoryService
    {
        private readonly IDataBaseContext _context;
        public DeleteCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long categoryId)
        {
                var category = _context.Categories.Where(c=>c.CategoryId==categoryId)
                    .Include(c=>c.SubCategories)
                    .FirstOrDefault();
                if (category != null)
                {
                    // find all sub categories and disable them
                    if (category.SubCategories.Any())
                        foreach (var item in category.SubCategories)
                        {
                            item.IsRemoved = true;
                            item.RemoveTime = DateTime.Now;
                        }
                    category.IsRemoved = true;
                    category.RemoveTime = DateTime.Now;

                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true, Message = $"{category.CategoryTitle} با موفقیت حذف شد !" };
                }
                return new ResultDto { Message = "داده ای پیدا نشد !" };
        }

    }
}
