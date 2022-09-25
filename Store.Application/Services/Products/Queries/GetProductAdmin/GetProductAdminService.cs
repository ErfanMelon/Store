﻿using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductAdmin
{
    public class GetProductAdminService : IGetProductAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetProductAdminDto> Execute(long productid)
        {
            try
            {
                var product = _context.Products.Where(p => p.ProductId == productid)
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.ProductImages)
                    .Include(p=>p.Category)
                    .ThenInclude(p=>p.ParentCategory)
                    .FirstOrDefault();
                if (product != null)
                {
                    return new ResultDto<GetProductAdminDto>
                    {
                        Data = new GetProductAdminDto
                        {
                            Brand = product.Brand,
                            Category = GetCategory(product.Category),
                            Description = product.Description,
                            Displayed = product.Displayed,
                            Features = product.ProductFeatures.ToList().Select(f => new GetProductFeatureDto
                            {
                                Feature = f.Feature,
                                FeatureValue = f.FeatureValue,
                            }).ToList(),
                            Images = product.ProductImages.ToList().Select(i => i.Src).ToList(),
                            Inventory = product.Inventory,
                            Price = product.Price,
                            ProductTitle = product.ProductTitle,
                        },
                        IsSuccess=true,
                        Message="",
                    };
                }
                return new ResultDto<GetProductAdminDto> { Message = "محصولی پیدا نشد !" };
            }
            catch (Exception)
            {
                return new ResultDto<GetProductAdminDto> { Message = "خطا رخ داد!" };
            }
        }
        string GetCategory(Category category)
        {
            if (category.ParentCategory != null)
                return $"{GetCategory(category.ParentCategory)} - {category.CategoryTitle}";
            return category.CategoryTitle;
        }
    }
}