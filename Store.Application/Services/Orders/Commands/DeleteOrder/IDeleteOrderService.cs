using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Orders.Commands.DeleteOrder
{
	public interface IDeleteOrderService
	{
		ResultDto Execute(long orederId);
	}
	public class DeleteOrderService: IDeleteOrderService
	{
		private readonly IDataBaseContext _context;
		public DeleteOrderService(IDataBaseContext context)
		{
			_context = context;
		}

		public ResultDto Execute(long orederId)
		{
			var order = _context.Orders
				.Include(d => d.OrderDetails)
				.Where(o => o.OrderId == orederId)
				.FirstOrDefault();
			
			if (order !=null)
			{
				foreach (var item in order.OrderDetails)
				{
					item.IsRemoved = true;
					item.RemoveTime = DateTime.Now;
				}
				order.IsRemoved = true;
                order.RemoveTime = DateTime.Now;
				_context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message="پاک شد" };
			}
			return new ResultDto { Message ="پیدا نشد"};
		}
	}
}
