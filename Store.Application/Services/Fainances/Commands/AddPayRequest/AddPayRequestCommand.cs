using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Users;

namespace Store.Application.Services.Fainances.Commands.AddPayRequest;
public class AddPayRequestCommand : IRequest<ResultDto<ResultAddPayRequest>>
{
    public AddPayRequestCommand(long userId, long cartId)
    {
        UserId = userId;
        CartId = cartId;
    }

    public long UserId { get; set; }
    public long CartId { get; set; }
    public class Handler : IRequestHandler<AddPayRequestCommand, ResultDto<ResultAddPayRequest>>
    {
        private readonly IDataBaseContext _context;
        private readonly UserValidator _validationRules;
        public Handler(IDataBaseContext context, UserValidator validationRules)
        {
            _context = context;
            _validationRules = validationRules;
        }
        public class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(e => e.Address).NotEmpty().WithMessage("آدرس معتبر نمیباشد");
                RuleFor(e => e.PhoneNumber).NotEmpty().WithMessage("شماره تماس معتبر نمیباشد");
                RuleFor(e => e.ZipCode).NotEmpty().WithMessage("کد پستی معتبر نمیباشد");
                RuleFor(e => e.IsActive).Equal(true).WithMessage("کاربر فعال نیست");
            }
        }
        public async Task<ResultDto<ResultAddPayRequest>> Handle(AddPayRequestCommand request, CancellationToken cancellationToken)
        {
            var requested_cart = await _context.Carts
                .Include(c => c.ItemsInCart)
                .ThenInclude(item => item.SelectedProduct)
                .Include(c => c.User)
                .SingleOrDefaultAsync(c => c.CartId == request.CartId);

            if ((requested_cart is null) || requested_cart.UserId != request.UserId)
                throw new ArgumentNullException("سبد پیدا نشد ");

            _validationRules.ValidateAndThrow(requested_cart.User);

            var requestpay = new RequestPay
            {
                User = requested_cart.User,
                PayId = Guid.NewGuid(),
                Cart = requested_cart,
                IsPay = false,
                Price = requested_cart.ItemsInCart.Sum(p => p.ProductCount * p.SelectedProduct.Price),
                Authority = ""
            };
            await _context.RequestPays.AddAsync(requestpay);
            await _context.SaveChangesAsync(cancellationToken);

            var result = new ResultAddPayRequest
            {
                RequestPayId = requestpay.PayId,
                TotalPrice = requestpay.Price,
                UserEmail = requested_cart.User.Email
            };
            return new ResultDto<ResultAddPayRequest>(result);    
        }
    }
}