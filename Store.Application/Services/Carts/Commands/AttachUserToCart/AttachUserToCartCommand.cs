using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;

namespace Store.Application.Services.Carts.Commands.AttachUserToCart;

public class AttachUserToCartCommand : IRequest
{
    public AttachUserToCartCommand(long userId, Guid browserId)
    {
        UserId = userId;
        BrowserId = browserId;
    }
    public long UserId { get; }
    public Guid BrowserId { get; }
    public class Validator : AbstractValidator<AttachUserToCartCommand>
    {
        public Validator()
        {
            RuleFor(e => e.BrowserId).NotEmpty();
            RuleFor(e => e.UserId).NotEmpty();
        }
    }
    public class Handler : IRequestHandler<AttachUserToCartCommand>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AttachUserToCartCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.UserId == request.UserId);
            
            if (user != null)
            {
                var oldActiveCart = await _context.Carts
                    .Include(c => c.ItemsInCart)
                    .Where(c => c.UserId == request.UserId)
                    .FirstOrDefaultAsync(c => c.CurrentCart);

                var cart = await _context.Carts
                    .Include(c=>c.ItemsInCart)
                    .SingleOrDefaultAsync(c => c.BrowserId == request.BrowserId && c.CurrentCart);

                if (cart != null)
                {
                    if(oldActiveCart !=null)
                    {
                        // Merge Two Cart
                        foreach (var product in oldActiveCart.ItemsInCart)
                        {
                           if(cart.ItemsInCart.Any(c=>c.ProductId==product.ProductId))
                            {
                                cart.ItemsInCart.First(p => p.ProductId == product.ProductId).ProductCount += product.ProductCount;
                            }
                           else
                            {
                                product.Cart = cart;
                            }
                        }
                        _context.Carts.Remove(oldActiveCart);
                    }
                    cart.User = user;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
