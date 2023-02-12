using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Orders.Commands.ChangeOrderState;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.ChangeOrderDetailState;
public class ToggleOrderDetailCommand : IRequest<ResultDto>
{
    public ToggleOrderDetailCommand(long orderDetailId, OrderState state)
    {
        OrderDetailId = orderDetailId;
        State = state;
    }
    public ToggleOrderDetailCommand()
    {

    }
    public long OrderDetailId { get; set; }
    public OrderState State { get; set; }
    public class Handler : IRequestHandler<ToggleOrderDetailCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IMediator _mediator;
        public Handler(IDataBaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto> Handle(ToggleOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _context.OrderDetails
                .Include(d => d.Order)
                .ThenInclude(o => o.OrderDetails)
                .SingleOrDefaultAsync(d => d.OrderDetailId == request.OrderDetailId);

            if (orderDetail is null)
                throw new ArgumentNullException("سفارش یافت نشد !");

            orderDetail.ProductState = request.State;

            switch (request.State)
            {
                case OrderState.InProccess:
                    break;
                case OrderState.Sending:
                    break;
                case OrderState.Cancelled:
                    break;
                case OrderState.Delivered:
                    OrderDetailDelivered(orderDetail);
                    break;
            }
            await _context.SaveChangesAsync(cancellationToken);

            if (orderDetail.Order.OrderDetails.All(p => p.ProductState == request.State))
            {
                await _mediator.Send(new ToggleOrderCommand(orderDetail.Order.OrderId, request.State));
            } 

            return new ResultDto(true, $"وضعیت {orderDetail.ProductName} با موفقیت به {EnumHelpers<OrderState>.GetDisplayValue(request.State)} تغییر کرد");
        }

        private static void OrderDetailDelivered(OrderDetail orderDetail)
        {
            orderDetail.DeliveredDate = DateTime.Now;
        }
    }
}