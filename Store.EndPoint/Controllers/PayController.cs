﻿using Dto.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Carts.Queries.GetCart;
using Store.Application.Services.Fainances.Commands.AddPayRequest;
using Store.Application.Services.Fainances.Commands.EditPayRequset;
using Store.EndPoint.Tools;
using ZarinPal.Class;

namespace Store.EndPoint.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRequestPayFacade _requestPayFacade;
        private readonly IOrderFacade _orderFacade;

        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        public PayController(IRequestPayFacade requestPayFacade, IOrderFacade orderFacade, IMediator mediator)
        {
            _requestPayFacade = requestPayFacade;
            _orderFacade = orderFacade;
            _mediator = mediator;

            #region ZarinPalCodes
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();

            #endregion

        }
        public async Task<IActionResult> Index()
        {
            long userId = ClaimTool.GetUserId(User).Value;
            var CustomerCart =await _mediator.Send(new GetCartQuery(userId));
            if (CustomerCart.IsSuccess && CustomerCart.Data.TotalPrice > 0)
            {
                var request = _requestPayFacade.addPayRequestService.Execute(new PayRequestDto
                {
                    CartId = CustomerCart.Data.CartId,
                    TransportPrice = 0,// other fees to add
                    UserId = userId
                });
                if (request.IsSuccess)
                {
                    #region ZarinPal Codes
                    var result = await _payment.Request(new DtoRequest()
                    {
                        Mobile = "0936000000",
                        CallbackUrl = $"https://localhost:44394/Pay/VerifyPayment?CustomerPaymentId={request.Data.RequestPayId}",
                        Description = "پرداخت فاکتور شماره:" + request.Data.RequestPayId,
                        Email = request.Data.UserEmail,
                        Amount = request.Data.TotalPrice,
                        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                    }, ZarinPal.Class.Payment.Mode.sandbox);
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
                    #endregion
                }

            }
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> VerifyPayment(Guid CustomerPaymentId, string authority, string status)
        {
            var requestDetail = _requestPayFacade.validateRequestPayService.Execute(CustomerPaymentId); // get price to check payment

            #region ZarinPal Codes
            //check payment
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestDetail.Data.Price,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);
            #endregion

            EditRequestPayDto result = new EditRequestPayDto
            {
                Description = verification.ExtraDetail,
                RefId = verification.RefId,
                RequsetPayId = CustomerPaymentId,
                Authority = authority
            }; //edit payment status and other info

            switch (verification.Status)
            {
                case 100:
                    result.IsPay = true;
                    _orderFacade.addOrderService.Execute(CustomerPaymentId);
                    break;
                default:
                    result.IsPay = false;
                    break;
            }
            _requestPayFacade.editRequsetPayService.Execute(result);
            if (status.ToLower() == "ok")
            {
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
