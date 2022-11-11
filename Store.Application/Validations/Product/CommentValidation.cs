using FluentValidation;
using Store.Application.Services.Products.Commands.AddComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Validations.Product
{
    public class CommentValidation:AbstractValidator<RequestAddComment>
    {
        public CommentValidation()
        {
            RuleFor(e=>e.CommentTitle).NotEmpty().WithMessage("عنوان نظر نباید خالی باشد");
            RuleFor(e => (int)e.Stars).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("امتیاز صحیح نمیباشد");
            RuleFor(e => e.Comment).NotEmpty().WithMessage("نظر را وارد کنید");
            RuleFor(e => e.Cons).NotEmpty().WithMessage("نقاط قوت را وارد کنید");
            RuleFor(e => e.Pros).NotEmpty().WithMessage("نقاط ضعف را وارد کنید");
            RuleFor(e => e.ProductId).NotEmpty().WithMessage("محصول یافت نشد");
            RuleFor(e => e.UserId).NotEmpty().WithMessage("کاربر یافت نشد");
        }
    }
}
