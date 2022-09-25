using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _dataBaseContext;
        public GetCategoriesService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public ResultDto<List<CategoryDto>> Execute()
        {
            try
            {
                var categories = _dataBaseContext.Categories
               .Include(p => p.ParentCategory)
               .Include(p => p.SubCategories)
               .ToList()
               .Select(p => new CategoryDto
               {
                   CategoryId = p.CategoryId,
                   CategoryTitle = p.CategoryTitle,
                   Parent = p.ParentCategory != null ? new
                   ParentCategoryDto
                   {
                       CategoryId = p.ParentCategory.CategoryId,
                       CategoryTitle = p.ParentCategory.CategoryTitle,
                   }
                   : null,
                   HasChild = p.SubCategories.Count() > 0 ? true : false,
               }).ToList();
                return new ResultDto<List<CategoryDto>> { Data = categories, IsSuccess = true, Message = "اطلاعات با موفقیت بارگیری شدند !" };
            }
            catch (Exception)
            {

                return new ResultDto<List<CategoryDto>> { Data = null, Message = "خطا" };
            }
        }
    }
}
