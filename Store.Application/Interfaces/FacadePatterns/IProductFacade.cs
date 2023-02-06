using Store.Application.Services.Products.Commands.AddComment;
using Store.Application.Services.Products.Queries.GetProductComments;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IProductFacade
    {
        public IAddCommentService addCommentService { get; }
        public IGetProductCommentsService getProductCommentsService { get; }
    }
}
