using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Fainances.Commands.DeletePayRequest
{
    public interface IDeletePayRequestService
    {
        ResultDto Execute(Guid payId);
    }
    public class DeletePayRequestService : IDeletePayRequestService
    {
        private readonly IDataBaseContext _context;
        public DeletePayRequestService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(Guid payId)
        {
            var pay = _context.RequestPays
                .Where(p => p.PayId == payId)
                .FirstOrDefault();
            if (pay != null)
            {
                var cart = _context.Carts.Find(pay.CartId);

                if (cart != null && !cart.CurrentCart)
                {
                    var incart = _context.ProductsInCarts.Any(p => p.CartId == cart.CartId) ? _context.ProductsInCarts.Where(p => p.CartId == cart.CartId).ToList() : new List<ProductsInCart>();
                    if (incart.Any())
                        foreach (var item in incart)
                        {
                            item.RemoveTime = DateTime.Now;
                            item.IsRemoved = true;
                        }
                    var order = _context.Orders.Where(o => o.PayId == payId)
                            .Include(d => d.OrderDetails)
                            .FirstOrDefault();
                    if (order != null)
                    {
                        foreach (var item in order.OrderDetails)
                        {
                            item.RemoveTime = DateTime.Now;
                            item.IsRemoved = true;
                        }
                        order.RemoveTime = DateTime.Now;
                        order.IsRemoved = true;
                    }
                    pay.Cart.RemoveTime = DateTime.Now;
                    pay.Cart.IsRemoved = true;
                }
                pay.IsRemoved = true;
                pay.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "پاک شد" };
            }
            return new ResultDto { Message = "پیدا نشد" };
        }
    }
}
