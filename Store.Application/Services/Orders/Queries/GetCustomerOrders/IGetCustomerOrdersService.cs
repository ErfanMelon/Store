using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrders
{
    public interface IGetCustomerOrdersService
    {
        ResultDto<List<CustomerOrdersDto>> Execute(long userId);
    }
    public class GetCustomerOrdersService : IGetCustomerOrdersService
    {
        private readonly IDataBaseContext _context;
        public GetCustomerOrdersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CustomerOrdersDto>> Execute(long userId)
        {
            var user = _context.Users.Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                .ThenInclude(o => o.OrderDetails)
                .FirstOrDefault();
            if (user != null)
            {
                List<CustomerOrdersDto> userOrders = new List<CustomerOrdersDto>();
                foreach (Order order in user.Orders)
                {
                    CustomerOrdersDto userOrder = new CustomerOrdersDto
                    {
                        OrderCreation = order.InsertTime,
                        OrderId = order.OrderId,
                        PayId = order.PayId,
                        PaidPrice=order.OrderDetails.Sum(d=>d.Amount*d.Count)
                    };
                    switch (order.OrderState)
                    {
                        case OrderState.InProccess:
                            userOrder.OrderState = "درحال ارسال";
                            break;
                        case OrderState.Cancelled:
                            userOrder.OrderState = "لغو شده";
                            break;
                        case OrderState.Delivered:
                            userOrder.OrderState = "تحویل داده شده";
                            break;
                        default:
                            userOrder.OrderState = "نامشخص";
                            break;
                    }
                    
                    userOrders.Add(userOrder);
                }
                return new ResultDto<List<CustomerOrdersDto>> { Data = userOrders, IsSuccess = true };
            }
            return new ResultDto<List<CustomerOrdersDto>>
            {
                Data = new List<CustomerOrdersDto>
                {
                    new CustomerOrdersDto
                    {
                        OrderCreation = DateTime.MinValue,
                    OrderId = 0,
                    OrderState = "",
                    PayId = Guid.Empty,
                    PaidPrice=0
                    }
                },
                Message = "کاربری یافت نشد !"
            };
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
    
}
