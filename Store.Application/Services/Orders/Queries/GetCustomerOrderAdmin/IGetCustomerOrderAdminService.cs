using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Orders.Queries.GetCustomerOrderAdmin;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrderAdmin
{
    public interface IGetCustomerOrderAdminService
	{
        ResultDto<GetCustomerOrderAdminDto> Execute(long orderId);
	}
	public class GetCustomerOrderAdminDto
	{
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid PayId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string OrderState { get; set; }
        public int Total { get; set; }
        public List<CustomerOrderDetailAdminDto> OrderDetails { get; set; }
        public string? PostCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
    public class CustomerOrderDetailAdminDto
    {
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public short Count { get; set; }
        public string OrderStateProduct { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Description { get; set; }
    }

    public class GetCustomerOrderAdminService: IGetCustomerOrderAdminService
	{
        private readonly IDataBaseContext _context;
        public GetCustomerOrderAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<GetCustomerOrderAdminDto> Execute(long orderId)
        {
            var order = _context.Orders.Where(o=>o.OrderId==orderId)
                .Include(o=>o.OrderDetails)
                .Include(o=>o.User)
                .Include(o=>o.RequestPay)
                .FirstOrDefault();
            if (order!=null)
            {
                GetCustomerOrderAdminDto result = new GetCustomerOrderAdminDto
                {
                    Address=order.User.Address,
                    CreationDate=order.InsertTime,
                    DeliveredDate=order.DeliveredDate,
                    Email=order.User.Email,
                    OrderId=order.OrderId,
                    PayId=order.PayId,
                    UserId=order.User.UserId,
                    UserName=order.User.UserFullName,
                    OrderState=EnumHelpers<OrderState>.GetDisplayValue(order.OrderState),
                    Total=order.RequestPay.Price,
                    PhoneNumber=order.User.PhoneNumber,
                    PostCode=order.User.ZipCode,
                    Description=order.Description
                };
                List<CustomerOrderDetailAdminDto> productInOrder = order.OrderDetails
                    .Select(d => new CustomerOrderDetailAdminDto
                    { 
                        Count=d.Count,
                        DeliveredDate=d.DeliveredDate,
                        OrderStateProduct=EnumHelpers<OrderState>.GetDisplayValue(d.ProductState),
                        Price=d.Amount*d.Count,
                        ProductId=d.ProductId,
                        ProductName=d.ProductName,
                        OrderDetailId=d.OrderDetailId,
                        Description=d.MoreDetail
                    }).ToList();
                result.OrderDetails = productInOrder;
                return new ResultDto<GetCustomerOrderAdminDto> { Data = result, IsSuccess = true };
            }
            return new ResultDto<GetCustomerOrderAdminDto> { Data = default, Message = "سفارشی پیدا نشد !" };
        }
    }
}
