using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Common.Queries.GetMenuCategories
{
    public interface IGetMenuCategoriesService
    {
        ResultDto<List<MenuCategoriesDto>> Execute();
    }
    public class GetMenuCategoriesService : IGetMenuCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetMenuCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<MenuCategoriesDto>> Execute()
        {
            var Categories = _context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategoryId == null)
                .Select(c => new MenuCategoriesDto
                {
                    CategoryId = c.CategoryId,
                    CategoryTitle = c.CategoryTitle,
                    HasChild = c.SubCategories.Any(),
                    Childs = c.SubCategories.Any() ? c.SubCategories.Select(child => new MenuCategoriesDto
                    {
                        CategoryId = child.CategoryId,
                        CategoryTitle = child.CategoryTitle,
                        HasChild = false
                    }).ToList() : null
                });
            return new ResultDto<List<MenuCategoriesDto>>
            {
                Data = Categories.ToList(),
                IsSuccess = true
            };
        }
    }
    public class MenuCategoriesDto
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public bool HasChild { get; set; } = false;
        public List<MenuCategoriesDto>? Childs { get; set; }
    }
}
