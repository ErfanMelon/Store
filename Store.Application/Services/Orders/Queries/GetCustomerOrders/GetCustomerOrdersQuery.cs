using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrders;
public class GetCustomerOrdersQuery : IRequest<ResultDto<List<CustomerOrdersDto>>>
{
    public GetCustomerOrdersQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; set; }
    public class Handler : IRequestHandler<GetCustomerOrdersQuery, ResultDto<List<CustomerOrdersDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<CustomerOrdersDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Orders)
                .ThenInclude(o => o.OrderDetails)
                .SingleOrDefaultAsync(u => u.UserId == request.UserId);

            if (user is null || !user.Orders.Any())
                throw new ArgumentNullException("سفارشی وجود ندارد");

            List<CustomerOrdersDto> userOrders = new List<CustomerOrdersDto>();
            foreach (Order order in user.Orders)
            {
                CustomerOrdersDto userOrder = new CustomerOrdersDto
                {
                    OrderCreation = order.InsertTime,
                    OrderId = order.OrderId,
                    PayId = order.PayId,
                    PaidPrice = order.OrderDetails.Sum(d => d.Amount * d.Count),
                    OrderState = EnumHelpers<OrderState>.GetDisplayValue(order.OrderState)
                };
                userOrders.Add(userOrder);
            }
            return new ResultDto<List<CustomerOrdersDto>>(userOrders);
        }
    }
}
public class CustomerOrdersDto
{
    public long OrderId { get; set; }
    public Guid PayId { get; set; }
    public string OrderState { get; set; }
    public DateTime OrderCreation { get; set; }
    public int PaidPrice { get; set; }
}

