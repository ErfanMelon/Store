using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.ChangeOrderDetailState
{
    public interface IChangeOrderDetailStateService
    {
        ResultDto Execute(long orderDetailId,OrderState orderState);
    }
    public class ChangeOrderDetailStateService: IChangeOrderDetailStateService
    {
        private readonly IDataBaseContext _context;
        public ChangeOrderDetailStateService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long orderDetailId, OrderState orderState)
        {
            var orderDetail = _context.OrderDetails.Where(d => d.OrderDetailId == orderDetailId)
                .Include(d=>d.Order)
                .ThenInclude(o=>o.OrderDetails)
                .FirstOrDefault();
            if (orderDetail!=null)
            {
                orderDetail.ProductState = orderState;
                if (orderState ==OrderState.Delivered)
                {
                    orderDetail.DeliveredDate = DateTime.Now;
                }
                orderDetail.UpdateTime = DateTime.Now;
                if (orderDetail.Order.OrderDetails.All(p=>p.ProductState==orderState))
                {
                    orderDetail.Order.OrderState = orderState;
                    orderDetail.Order.UpdateTime = DateTime.Now;
                    if (orderDetail.Order.OrderState==OrderState.Delivered)
                    {
                        orderDetail.Order.DeliveredDate = DateTime.Now;
                    }
                }
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"وضعیت {orderDetail.ProductName} با موفقیت به {EnumHelpers<OrderState>.GetDisplayValue(orderState)} تغییر کرد" };
            }
            return new ResultDto { Message = "سفارش یافت نشد !" };
        }
    }
}
