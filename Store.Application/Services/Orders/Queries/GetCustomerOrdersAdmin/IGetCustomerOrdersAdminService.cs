using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrdersAdmin
{
    public interface IGetCustomerOrdersAdminService
    {
        ResultDto<CustomerOrdersAdminDto> Execute(OrderState? orderState, string? SearchKey, int page, int pagesize);
    }
    public class CustomerOrdersAdminDto
    {
        public List<GetCustomerOrdersAdminDto> CustomersOrders { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
    }
    public class GetCustomerOrdersAdminDto
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string OrderState { get; set; }
        public DateTime OrderCreation { get; set; }
        public int PaidPrice { get; set; }
        public string? Description { get; set; }
    }
    public class GetCustomerOrdersAdminService : IGetCustomerOrdersAdminService
    {
        private readonly IDataBaseContext _context;
        public GetCustomerOrdersAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CustomerOrdersAdminDto> Execute(OrderState? orderState, string? SearchKey, int page, int pagesize)
        {
            var orders = _context.Orders
                .Include(o=>o.User)
                .AsQueryable();
            if (orderState != null)
            {
                orders = orders.Where(o => o.OrderState == orderState).AsQueryable();
            }
            if (!string.IsNullOrEmpty(SearchKey))
            {
                orders = orders.Where(o =>
                o.InsertTime.ToString().Contains(SearchKey)||
                o.PayId.ToString().Contains(SearchKey)
                ).AsQueryable();
            }
            var orderlist = orders.Select(o => new GetCustomerOrdersAdminDto
            {
                OrderCreation = o.InsertTime,
                OrderId = o.OrderId,
                OrderState = EnumHelpers<OrderState>.GetDisplayValue(o.OrderState),
                PaidPrice = o.RequestPay.Price,
                UserId = o.UserId,
                UserName=o.User.UserFullName,
                Description=o.Description

            }).ToPaged(page, pagesize, out int rowCount).ToList();
            if (orders.Any())
            {
                return new ResultDto<CustomerOrdersAdminDto>
                {
                    Data = new CustomerOrdersAdminDto
                    {
                        CurrentPage = page,
                        CustomersOrders = orderlist,
                        PageSize = pagesize,
                        RowsCount = rowCount
                    },
                    IsSuccess = true
                };
            }
            return new ResultDto<CustomerOrdersAdminDto> { Data  = new CustomerOrdersAdminDto
            {
                CurrentPage = page,
                CustomersOrders = new List<GetCustomerOrdersAdminDto>(),
                PageSize = pagesize,
                RowsCount = rowCount
            }, Message = "داده ای پیدا نشد !" };
        }
    }
}
