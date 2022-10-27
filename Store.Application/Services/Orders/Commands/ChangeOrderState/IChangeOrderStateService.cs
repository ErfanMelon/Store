using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.ChangeOrderState
{
    public interface IChangeOrderStateService
    {
        ResultDto Execute(long orderId, OrderState state);
    }
    public class ChangeOrderStateService: IChangeOrderStateService
    {
        private readonly IDataBaseContext _context;
        public ChangeOrderStateService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long orderId, OrderState state)
        {
            var order = _context.Orders.Where(o => o.OrderId == orderId)
                .Include(o => o.OrderDetails)
                .FirstOrDefault();
            if (order!=null)
            {
                order.OrderState = state;
                if (state==OrderState.Delivered)
                {
                    order.DeliveredDate = DateTime.Now;
                }
                foreach (var item in order.OrderDetails)
                {
                    item.ProductState = state;
                    item.UpdateTime = DateTime.Now;
                    item.DeliveredDate = order.DeliveredDate;
                }
                
                order.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"سفارش شماره {order.OrderId} با موفقیت به {EnumHelpers<OrderState>.GetDisplayValue(state)} تغییر کرد !" };
            }
            return new ResultDto { Message = "سفارش پیدا نشد" };
        }
    }
}
