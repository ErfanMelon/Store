using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Queries.GetProductComments
{
    public interface IGetProductCommentsService
    {
        ResultDto<GetCommentsDto> Execute(long productId, int page, int pagesize);
    }
    public class GetCommentsDto
    {
        public List<CommentDto> Comments { get; set; }
        public int RowSize { get; set; }
    }
    public class CommentDto
    {
        public string UserName { get; set; }
        public DateTime PostDate { get; set; }
        public string CommentTitle { get; set; }
        public short Stars { get; set; }
        public string Comment { get; set; }
        public List<string> Pros { get; set; }
        public List<string> Cons { get; set; }
    }
    public class GetProductCommentsService : IGetProductCommentsService
    {
        private readonly IDataBaseContext _context;
        public GetProductCommentsService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetCommentsDto> Execute(long productId, int page, int pagesize)
        {
            var comments = _context.Comments
                .Include(c => c.User)
                .Where(c => c.ProductId == productId)
                .ToPaged(page,pagesize,out int rows).ToList();
            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (Comment c in comments)
            {
                CommentDto comment = new CommentDto();
                comment.Comment = c.UserComment;
                comment.CommentTitle = c.Title;
                comment.Cons = c.Cons.Split(';').ToList();
                comment.PostDate = c.InsertTime;
                comment.Pros = c.Pros.Split(';').ToList();
                comment.Stars = Convert.ToInt16(c.Score);
                comment.UserName = c.User.UserFullName;

                commentDtos.Add(comment);
            }

            //    comments.Select(p => new CommentDto
            //{
            //    Comment = p.UserComment,
            //    CommentTitle = p.Title,
            //    Cons = p.Cons.Split(';', ';').ToList(),
            //    PostDate = p.InsertTime,
            //    Pros = p.Pros.Split(';',';').ToList(),
            //    Stars = Convert.ToInt16(p.Score),
            //    UserName = p.User.UserFullName
            //}).ToPaged(page, pagesize, out int rows).ToList();
            return new ResultDto<GetCommentsDto>
            {
                Data = new GetCommentsDto
                {
                    Comments = commentDtos,
                    RowSize = rows
                },
                IsSuccess = true
            };
        }
    }
}
