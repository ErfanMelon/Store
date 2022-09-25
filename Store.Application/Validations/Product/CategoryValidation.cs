using FluentValidation;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.Application.Services.Products.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Validations.Product
{
    public class CategoryValidation:AbstractValidator<AddCategoryDto>
    {
        public CategoryValidation()
        {
            RuleFor(e => e.CategoryTitle).NotEmpty().WithMessage("لطفا عنوان دسته بندی را وارد کنید !");
            RuleFor(e => e.CategoryTitle).MaximumLength(200).WithMessage("عنوان دسته بندی نباید از 200 کاراکتر بیشتر باشد !");
        }
    }
    public class EditCategoryValidation:AbstractValidator<EditCategoryDto>
    {
        public EditCategoryValidation()
        {
            RuleFor(e => e.CategoryTitle).NotEmpty().WithMessage("لطفا عنوان دسته بندی را وارد کنید !");
            RuleFor(e => e.CategoryTitle).MaximumLength(200).WithMessage("عنوان دسته بندی نباید از 200 کاراکتر بیشتر باشد !");
            RuleFor(e => e.CategoryId).NotEqual(e=>e.ParentCategoryId.Value).When(e=>e.ParentCategoryId.HasValue).WithMessage("دسته بندی نمیتواند زیرمجموعه خودش باشد !");
        }
    }
}
