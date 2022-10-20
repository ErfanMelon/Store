using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Users;

namespace Store.Application.Services.Fainances.Commands.AddPayRequest
{
    public interface IAddPayRequestService
    {
        ResultDto<ResultAddPayRequest> Execute(PayRequestDto request);
    }
    public class AddPayRequestService : IAddPayRequestService
    {
        private readonly IDataBaseContext _context;
        public AddPayRequestService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultAddPayRequest> Execute(PayRequestDto request)
        {
            Cart requested_cart = _context.Carts.Where(c => c.CartId == request.CartId)
                .Include(c => c.ItemsInCart)
                .ThenInclude(item => item.SelectedProduct)
                .FirstOrDefault();
            if (requested_cart != null && requested_cart.UserId == request.UserId)
            {
                User customer = _context.Users.Find(request.UserId);
                var requestpay = new RequestPay
                {
                    User = customer,
                    PayId=Guid.NewGuid(),
                    Cart = requested_cart,
                    IsPay = false,
                    Price = requested_cart.ItemsInCart.Sum(p => p.ProductCount * p.SelectedProduct.Price) + request.TransportPrice,
                    Authority=""
                };
                _context.RequestPays.Add(requestpay);
                _context.SaveChanges();
                return new ResultDto<ResultAddPayRequest>
                {
                    Data = new ResultAddPayRequest
                    {
                        RequestPayId= requestpay.PayId,
                        TotalPrice= requestpay.Price,
                        UserEmail=customer.Email
                    },
                    IsSuccess = true,
                    Message = "درخواست اضافه شد !"
                };
            }
            return new ResultDto<ResultAddPayRequest> { Data = new ResultAddPayRequest
            {
                RequestPayId = Guid.Empty,
                TotalPrice = 0,
                UserEmail = ""
            },
                Message = "سبد پیدا نشد !" };
        }
    }
    public class PayRequestDto
    {
        public long UserId { get; set; }
        public long CartId { get; set; }
        public int TransportPrice { get; set; } = 0;
    }
    public class ResultAddPayRequest
    {
        public int TotalPrice { get; set; }
        public Guid RequestPayId { get; set; }
        public string UserEmail { get; set; }
    }
}
