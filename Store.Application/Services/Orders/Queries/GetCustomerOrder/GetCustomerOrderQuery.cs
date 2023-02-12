using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrder;
public class GetCustomerOrderQuery : IRequest<ResultDto<List<CustomerOrdersProductDto>>>
{
    public GetCustomerOrderQuery(long userId, long orderId)
    {
        UserId = userId;
        OrderId = orderId;
    }

    [BindNever]
    public long UserId { get; set; }
    public long OrderId { get; set; }
    public class Handler : IRequestHandler<GetCustomerOrderQuery, ResultDto<List<CustomerOrdersProductDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<CustomerOrdersProductDto>>> Handle(GetCustomerOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(o => o.OrderId == request.OrderId);

            if (order is null || order.UserId != request.UserId)
                throw new Exception("فاکتور پیدا نشد !");

            List<CustomerOrdersProductDto> ProductDetails = new List<CustomerOrdersProductDto>();
            foreach (OrderDetail detail in order.OrderDetails)
            {
                CustomerOrdersProductDto details = new CustomerOrdersProductDto
                {
                    ProductId = detail.ProductId,
                    Count = detail.Count,
                    Price = detail.Amount,
                    ProductName = detail.ProductName,
                    ProductImg = _context.ProductImages.FirstOrDefault(i => i.ProductId == detail.ProductId)?.Src ?? "",
                    OrderStateProduct = EnumHelpers<OrderState>.GetDisplayValue(detail.ProductState)
                };
                ProductDetails.Add(details);
            }

            return new ResultDto<List<CustomerOrdersProductDto>>(ProductDetails);
        }
    }
}
public class CustomerOrdersProductDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public short Count { get; set; }
    public string OrderStateProduct { get; set; }
    public string ProductImg { get; set; }
}
