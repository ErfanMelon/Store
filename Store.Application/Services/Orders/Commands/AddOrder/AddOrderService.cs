using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Commands.AddOrder
{
    public class AddOrderService : IAddOrderService
    {
        private readonly IDataBaseContext _context;
        public AddOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(Guid requestPayId)
        {
            var requestPay = _context.RequestPays.Where(e => e.PayId == requestPayId)
                .Include(e => e.Cart)
                .ThenInclude(p => p.ItemsInCart)
                .ThenInclude(s => s.SelectedProduct)
                .Include(e => e.User)
                .FirstOrDefault();
            if (requestPay != null)
            {
                Order order = new Order
                {
                    RequestPay = requestPay,
                    OrderRefund = 0,
                    PayId = requestPay.PayId,
                    User = requestPay.User,
                    OrderState = OrderState.InProccess,
                    UserAddress=requestPay.User.Address
                    
                };
                _context.Orders.Add(order);
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (ProductsInCart product in requestPay.Cart.ItemsInCart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        Order = order,
                        Amount = product.SelectedProduct.Price,
                        Count = product.ProductCount,
                        ProductId = product.SelectedProduct.ProductId,
                        ProductName = product.SelectedProduct.ProductTitle,
                        ProductRefund = 0,
                        ProductState = OrderState.InProccess,
                    };
                    orderDetails.Add(orderDetail);
                }
                _context.OrderDetails.AddRange(orderDetails);
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "سفارش ثبت شد" };
            }
            return new ResultDto { Message = "خرید صورت نگرفته است !" };
        }
    }
}
