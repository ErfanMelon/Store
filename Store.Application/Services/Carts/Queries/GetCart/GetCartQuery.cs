using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Carts.Commands.CreateCart;
using Store.Common.Dto;

namespace Store.Application.Services.Carts.Queries.GetCart;
public class CartDto
{
    public CartDto(long cartId, List<CartProductDto> productDtos)
    {
        CartId = cartId;
        ProductDtos = productDtos;
        TotalPrice = ProductDtos.Any() ? ProductDtos.Sum(e => e.Count * e.Price) : 0;
    }

    public long CartId { get; }
    public List<CartProductDto> ProductDtos { get; }
    public int TotalPrice { get; }

}
public class CartProductDto
{
    public string ProductImg { get; set; }
    public long ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Price { get; set; }
    public short Count { get; set; }
}
public class GetCartQuery : IRequest<ResultDto<CartDto>>
{
    public GetCartQuery(Guid? browserId, long? userId = null)
    {
        UserId = userId;
        BrowserId = browserId;
    }
    public GetCartQuery(long userId)
    {
        UserId = userId;
    }

    public long? UserId { get; }
    public Guid? BrowserId { get; }

    public class Validator : AbstractValidator<GetCartQuery>
    {
        public Validator()
        {
            // Both UserId and BrowserId can't be null at same time.
            RuleFor(e => e.BrowserId).NotEmpty().When(e => e.UserId.HasValue == false);
            RuleFor(e => e.UserId).NotEmpty().When(e => e.BrowserId.HasValue == false);
        }
    }
    public class Handler : IRequestHandler<GetCartQuery, ResultDto<CartDto>>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;

        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Carts
                .AsNoTracking()
                .Include(c => c.ItemsInCart)
                .ThenInclude(p => p.SelectedProduct)
                .ThenInclude(p => p.ProductImages)
                .AsQueryable();

            if (request.BrowserId.HasValue) // search by BrowserId
                query = query.Where(c => c.BrowserId == request.BrowserId);

            if (request.UserId.HasValue) // search by UserId
                query = query.Where(c => c.UserId == request.UserId);

            var cart = await query.FirstOrDefaultAsync(c => c.CurrentCart); // return Active cart

            if (cart is null) // if cart doesn't exist create new cart
            {
                var cartId = await _mediator.Send(new CreateCartCommand(request.BrowserId, request.UserId));
                cart = await _context.Carts.FindAsync(cartId.Data);
            }
            if (cart is null) // if cart still doesn't exist raise an Error
                throw new NullReferenceException();

            CartDto result;
            List<CartProductDto> products = new List<CartProductDto>(); // user's products

            if (cart.ItemsInCart?.Any()??false)
                products = cart.ItemsInCart.Select(p =>
                new CartProductDto
                {
                    Count = p.ProductCount,
                    Price = p.SelectedProduct.Price,
                    ProductId = p.ProductId,
                    ProductImg = p.SelectedProduct.ProductImages.FirstOrDefault()?.Src ?? "",
                    ProductTitle = p.SelectedProduct.ProductTitle
                }).ToList();

            result = new CartDto(cart.CartId, products);

            return new ResultDto<CartDto>(result);
        }
    }
}
