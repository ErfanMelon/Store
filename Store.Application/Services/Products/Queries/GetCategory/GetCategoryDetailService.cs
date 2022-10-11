using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategory
{
    public class GetCategoryDetailService : IGetCategoryDetailService
    {
        private readonly IDataBaseContext _context;
        public GetCategoryDetailService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CategoryDetailDto> Execute(long categoryId)
        {
            
                var resultCategory = _context.Categories.Where(c => c.CategoryId == categoryId)
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefault();
                if (resultCategory != null)
                {
                    ParentCategoryDto parent = new ParentCategoryDto();
                    if (resultCategory.ParentCategory != null)
                    {
                        parent.CategoryTitle = resultCategory.ParentCategory.CategoryTitle;
                        parent.CategoryId = resultCategory.ParentCategory.CategoryId;
                    }
                    return new ResultDto<CategoryDetailDto>
                    {
                        Data = new CategoryDetailDto
                        {
                            CategoryId = resultCategory.CategoryId,
                            CategoryTitle = resultCategory.CategoryTitle,
                            HasChild = resultCategory.SubCategories.Any(),
                            Parent = resultCategory.ParentCategory != null ? new ParentCategoryDto 
                            { 
                                CategoryId = resultCategory.ParentCategory.CategoryId,
                                CategoryTitle = resultCategory.ParentCategory.CategoryTitle
                            } : null,
                            SubCategories = resultCategory.SubCategories.Any() ? resultCategory.SubCategories.ToList().Select(s => new ParentCategoryDto
                            {
                                CategoryId = s.CategoryId,
                                CategoryTitle = s.CategoryTitle,
                            }).ToList() : null
                        },
                        IsSuccess = true,
                        Message = "داده بارگزاری شد !"
                    };
                }
                return new ResultDto<CategoryDetailDto> { Message = "داده ای پیدا نشد !" };
            
        }
    }


}
