using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Users;

namespace Store.Application.Services.Fainances.Commands.AddPayRequest
{
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
                .Include(c => c.User)
                .FirstOrDefault();
            if (requested_cart != null && requested_cart.UserId == request.UserId)
            {
                #region FluentValidationLater
                if (requested_cart.User.Address == null)
                {
                    return new ResultDto<ResultAddPayRequest> { Message = "آدرس تحویل صحیح نمیباشد !" };
                }
                if (requested_cart.User.PhoneNumber == null)
                {
                    return new ResultDto<ResultAddPayRequest> { Message = "شماره تلفن صحیح نمیباشد !" };
                }
                if (requested_cart.User.ZipCode == null)
                {
                    return new ResultDto<ResultAddPayRequest> { Message = "کد پستی صحیح نمیباشد !" };
                }
                #endregion

                var requestpay = new RequestPay
                {
                    User = requested_cart.User,
                    PayId = Guid.NewGuid(),
                    Cart = requested_cart,
                    IsPay = false,
                    Price = requested_cart.ItemsInCart.Sum(p => p.ProductCount * p.SelectedProduct.Price) + request.TransportPrice,
                    Authority = ""
                };
                _context.RequestPays.Add(requestpay);
                _context.SaveChanges();
                return new ResultDto<ResultAddPayRequest>
                {
                    Data = new ResultAddPayRequest
                    {
                        RequestPayId = requestpay.PayId,
                        TotalPrice = requestpay.Price,
                        UserEmail = requested_cart.User.Email
                    },
                    IsSuccess = true,
                    Message = "درخواست اضافه شد !"
                };
            }
            return new ResultDto<ResultAddPayRequest>
            {
                Message = "سبد پیدا نشد !"
            };
        }
    }
}
