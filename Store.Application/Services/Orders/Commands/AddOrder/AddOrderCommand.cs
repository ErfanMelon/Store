using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.AddOrder;
public class AddOrderCommand : IRequest<ResultDto>
{
    public AddOrderCommand(Guid payId)
    {
        PayId = payId;
    }

    public Guid PayId { get; set; }
    public class Handler : IRequestHandler<AddOrderCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var pay = await _context.RequestPays
                .Include(e => e.Cart)
                .ThenInclude(p => p.ItemsInCart)
                .ThenInclude(s => s.SelectedProduct)
                .Include(e => e.User)
                .SingleOrDefaultAsync(e => e.PayId == request.PayId);

            if (pay is null)
                throw new ArgumentNullException("خرید صورت نگرفته است ");


            Order order = new Order
            {
                RequestPay = pay,
                OrderRefund = 0,
                User = pay.User,
                OrderState = OrderState.InProccess,
                UserAddress = pay.User.Address,
                ZipCode = pay.User.ZipCode
            };
            await _context.Orders.AddAsync(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (ProductsInCart product in pay.Cart.ItemsInCart)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    Order = order,
                    Amount = product.SelectedProduct.Price,
                    Count = product.ProductCount,
                    ProductId = product.SelectedProduct.ProductId,
                    ProductName = product.SelectedProduct.ProductTitle,
                    ProductRefund = 0,
                    ProductState = OrderState.InProccess,
                };
                orderDetails.Add(orderDetail);
            }

            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, "سفارش ثبت شد");
        }
    }
}
