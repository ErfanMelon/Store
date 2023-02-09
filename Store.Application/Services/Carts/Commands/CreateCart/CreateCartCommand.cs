using FluentValidation;
using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;

namespace Store.Application.Services.Carts.Commands.CreateCart;

public class CreateCartCommand : IRequest<ResultDto<long>>
{
    public CreateCartCommand(Guid? browserId, long? userId = null)
    {
        UserId = userId;
        BrowserId = browserId;
    }
    public CreateCartCommand(long userId)
    {
        UserId = userId;
    }

    public long? UserId { get; }
    public Guid? BrowserId { get; }
    public class Validator : AbstractValidator<CreateCartCommand>
    {
        public Validator()
        {
            // Both UserId and BrowserId can't be null at same time.
            RuleFor(e => e.BrowserId).NotEmpty().When(e => e.UserId.HasValue == false);
            RuleFor(e => e.UserId).NotEmpty().When(e => e.BrowserId.HasValue == false);
        }
    }
    public class Handler : IRequestHandler<CreateCartCommand, ResultDto<long>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<long>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart
            {
                CurrentCart = true
            };

            if (request.UserId.HasValue)
                cart.User = await _context.Users.FindAsync(request.UserId);

            if (request.BrowserId.HasValue)
                cart.BrowserId = request.BrowserId.Value;

            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto<long>(cart.CartId);
        }
    }
}
