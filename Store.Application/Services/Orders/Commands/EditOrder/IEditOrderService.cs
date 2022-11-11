using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Orders.Commands.EditOrder
{
    public interface IEditOrderService
    {
        ResultDto Execute(RequestEditOrderDto request);
    }
    public class RequestEditOrderDto
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
    }
    public class EditOrderService: IEditOrderService
    {
        private readonly IDataBaseContext _context;
        public EditOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditOrderDto request)
        {
            var order = _context.Orders.Find(request.OrderId);
            if (order!=null)
            {
                order.Description = request.Description;
                order.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "با موفقیت ویرایش شد" };
            }
            return new ResultDto { Message ="سفارش پیدا نشد "};
        }
    }
}
