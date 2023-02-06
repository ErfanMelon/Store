using MediatR;
using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddComment;
using Store.Application.Services.Products.Queries.GetProductComments;

namespace Store.Application.Services.Products.Facade
{
    public class ProductFacade : IProductFacade
    {
        private readonly IDataBaseContext _context;
        public ProductFacade(IDataBaseContext context)
        {
            _context = context;
        }
        private IAddCommentService _addCommentService;
        public IAddCommentService addCommentService
        {
            get
            {
                return _addCommentService = _addCommentService ?? new AddCommentService(_context);
            }
        }
        private IGetProductCommentsService _getProductCommentsService;
        public IGetProductCommentsService getProductCommentsService
        {
            get
            {
                return _getProductCommentsService = _getProductCommentsService ?? new GetProductCommentsService(_context);
            }
        }
    }
}
