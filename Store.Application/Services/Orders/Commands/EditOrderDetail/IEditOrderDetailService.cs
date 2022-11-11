using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.Commands.EditOrderDetail
{
    public interface IEditOrderDetailService
    {
        ResultDto Execute(RequestEditOrderDetailDto request);
    }
    public class RequestEditOrderDetailDto
    {
        public long OrderDetailId { get; set; }
        public short Count { get; set; }
        public string? Description { get; set; }
    }
    public class EditOrderDetailService : IEditOrderDetailService
    {
        private readonly IDataBaseContext _context;
        public EditOrderDetailService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditOrderDetailDto request)
        {
            var orderdetail = _context.OrderDetails
                .Include(o => o.Order)
                .Where(d => d.OrderDetailId == request.OrderDetailId)
                .FirstOrDefault();
            if (orderdetail != null)
            {
                orderdetail.MoreDetail = request.Description;
                if (orderdetail.Count != request.Count && request.Count >= 0)
                {
                    int totalspend = orderdetail.Count * orderdetail.Amount;
                    orderdetail.Count = request.Count;
                    orderdetail.ProductRefund = totalspend - (orderdetail.Count * orderdetail.Amount);
                    orderdetail.Order.OrderRefund += orderdetail.ProductRefund;
                    orderdetail.Order.UpdateTime = DateTime.Now;
                    orderdetail.UpdateTime = DateTime.Now;
                    if (orderdetail.Order.OrderRefund<0 || orderdetail.ProductRefund<0)
                    {
                        return new ResultDto { Message = "کاربر بدهکار میباشد" };
                    }
                }  
                _context.SaveChanges();

                return new ResultDto { IsSuccess = true, Message = "ویرایش با موفقیت انجام شد !" };


            }
            return new ResultDto { Message = "سفارش یافت نشد !" };
        }
    }
}
