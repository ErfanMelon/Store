using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Carts.Commands.CreateCart;
using Store.Common.Dto;

namespace Store.Application.Services.Carts.Commands.EditCartProduct;

/// <summary>
/// Edit User's Cart's Products
/// </summary>
public class EditCartProductCommand : IRequest<ResultDto>
{
    [BindNever]
    public Guid? BrowserId { get; set; }
    [BindNever]
    public long? UserId { get; set; }
    public long ProductId { get; set; }
    public short ProductCount { get; set; }
    public class Handler : IRequestHandler<EditCartProductCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;

        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto> Handle(EditCartProductCommand request, CancellationToken cancellationToken)
        {
            var query = _context.Carts
                .Include(c => c.ItemsInCart)
                .ThenInclude(p => p.SelectedProduct)
                .AsQueryable();

            if (request.BrowserId.HasValue) // search cart By BrowserId
                query = query.Where(c => c.BrowserId == request.BrowserId);

            if (request.UserId.HasValue) // search cart By UserId
                query = query.Where(c => c.UserId == request.UserId);

            var cart = await query.FirstOrDefaultAsync(c => c.CurrentCart); // return active cart

            if (cart is null) // if cart doesn't exist create new cart
            {
                var cartId = await _mediator.Send(new CreateCartCommand(request.BrowserId, request.UserId));
                cart = await _context.Carts
                    .SingleOrDefaultAsync(c => c.CartId == cartId.Data);
            }
            if (cart is null) // if cart still doesn't exist raise an Error
                throw new NullReferenceException();

            var product = cart.ItemsInCart.SingleOrDefault(p => p.ProductId == request.ProductId);
            if (product is null) // this product doesn't exist in cart
            {
                var wantedProduct = await _context.Products // find product in database
                    .SingleOrDefaultAsync(p => p.ProductId == request.ProductId);

                if (wantedProduct is null) // this product doesn't exist
                    throw new ArgumentNullException("محصول معتبر نیست");

                product = new Domain.Entities.Carts.ProductsInCart
                {
                    Cart = cart,
                    ProductCount = request.ProductCount,
                    SelectedProduct = wantedProduct,
                };
                await _context.ProductsInCarts.AddAsync(product);
            }
            if (request.ProductCount <= 0)// Should delete product from cart
            {
                _context.ProductsInCarts.Remove(product);
            }
            else
            {
                product.ProductCount = request.ProductCount;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto(true);
        }
    }
}
