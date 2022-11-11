using Store.Application.Interfaces.Context;
using Store.Application.Validations.Product;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddComment
{
    public interface IAddCommentService
    {
        ResultDto Execute(RequestAddComment request);
    }
    public class AddCommentService:IAddCommentService
    {
        private readonly IDataBaseContext _context;
        public AddCommentService(IDataBaseContext context)
        {
            _context=context;
        }

        public ResultDto Execute(RequestAddComment request)
        {
            CommentValidation validationRules = new CommentValidation();
           var valid= validationRules.Validate(request);
            if (!valid.IsValid)
            {
                return new ResultDto { Message = valid.Errors[0].ErrorMessage };
            }
            Comment comment = new Comment
            {
                ProductId=request.ProductId,
                Score=request.Stars,
                Title=request.CommentTitle,
                UserId=request.UserId,
                UserComment=request.Comment,
            };

            comment.Pros = string.Join(';', request.Pros.Where(p => !p.Contains(';')));
            comment.Cons = string.Join(';', request.Cons.Where(c => !c.Contains(';')));
            //string pros="", cons="";
            //foreach (string pro in request.Pros)
            //{
            //    if (pro.Contains(';'))
            //    {
            //        continue;
            //    }
            //    pros += $"{pro};";
            //}
            //foreach (string con in request.Cons)
            //{
            //    if (con.Contains(';'))
            //    {
            //        continue;
            //    }
            //    cons += $"{con};";
            //}
            //comment.Cons = cons;
            //comment.Pros = pros;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return new ResultDto { IsSuccess = true, Message = "نظر شما ثبت شد" };
        }
    }
        public class RequestAddComment
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
        public short Stars { get; set; }
        public List<string> Pros { get; set; }
        public List<string> Cons { get; set; }
    }
}
