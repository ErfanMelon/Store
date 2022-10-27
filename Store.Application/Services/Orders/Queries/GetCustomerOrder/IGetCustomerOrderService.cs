using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrder
{
    public interface IGetCustomerOrderService
    {
        ResultDto<List<CustomerOrdersProductDto>> Execute(long userId, long orderId);
    }
    public class GetCustomerOrderService : IGetCustomerOrderService
    {
        private readonly IDataBaseContext _context;
        public GetCustomerOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CustomerOrdersProductDto>> Execute(long userId, long orderId)
        {
            var order = _context.Orders.Where(o => o.OrderId == orderId && o.UserId == userId)
                .Include(o => o.OrderDetails)
                .FirstOrDefault();
            if (order != null)
            {
                List<CustomerOrdersProductDto> ProductDetails = new List<CustomerOrdersProductDto>();
                foreach (OrderDetail detail in order.OrderDetails)
                {
                    CustomerOrdersProductDto details = new CustomerOrdersProductDto
                    {
                        ProductId = detail.ProductId,
                        Count = detail.Count,
                        Price = detail.Amount,
                        ProductName = detail.ProductName,
                        ProductImg=_context.ProductImages.Where(i=>i.ProductId==detail.ProductId).FirstOrDefault()?.Src
                    };
                    switch (detail.ProductState)
                    {
                        case OrderState.InProccess:
                            details.OrderStateProduct = "درحال ارسال";
                            break;
                        case OrderState.Cancelled:
                            details.OrderStateProduct = "لغو شده";
                            break;
                        case OrderState.Delivered:
                            details.OrderStateProduct = "تحویل داده شده";
                            break;
                        default:
                            details.OrderStateProduct = "نامشخص";
                            break;
                    }
                    ProductDetails.Add(details);
                }
                return new ResultDto<List<CustomerOrdersProductDto>>
                {
                    Data = ProductDetails,
                    IsSuccess = true
                };
            }
            return new ResultDto<List<CustomerOrdersProductDto>> { Data = default, Message = "فاکتور پیدا نشد !" };
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
}
