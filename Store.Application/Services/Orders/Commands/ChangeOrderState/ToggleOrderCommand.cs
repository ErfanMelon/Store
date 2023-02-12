using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.ChangeOrderState;
public class ToggleOrderCommand : IRequest<ResultDto>
{
    public long OrderId { get; set; }
    public OrderState State { get; set; }
    public ToggleOrderCommand()
    {

    }

    public ToggleOrderCommand(long orderId, OrderState state)
    {
        OrderId = orderId;
        State = state;
    }
    public class Handler : IRequestHandler<ToggleOrderCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(ToggleOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(o => o.OrderId == request.OrderId);
            if (order is null)
                throw new ArgumentNullException("سفارش پیدا نشد");

            order.OrderState = request.State;

            switch (request.State)
            {
                case OrderState.InProccess:
                    break;
                case OrderState.Sending:
                    break;
                case OrderState.Cancelled:
                    break;
                case OrderState.Delivered:
                    OrderDelivered(order);
                    break;
                default:
                    break;
            }
             

            foreach (OrderDetail detail in order.OrderDetails) // set all Order Details' State to Request.State
            {
                detail.ProductState = request.State;
            }
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, $"سفارش شماره {order.OrderId} با موفقیت به {EnumHelpers<OrderState>.GetDisplayValue(request.State)} تغییر کرد !");
        }

        private static void OrderDelivered(Domain.Entities.Orders.Order order)
        {
            order.OrderDetails.Select(d => d.DeliveredDate = DateTime.Now);
        }
    }
}
