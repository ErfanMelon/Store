﻿using Dto.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts.Queries.GetCart;
using Store.Application.Services.Fainances.Commands.AddPayRequest;
using Store.Application.Services.Fainances.Commands.EditPayRequset;
using Store.Application.Services.Fainances.Queries.VaildateRequestPay;
using Store.Application.Services.Orders.Commands.AddOrder;
using Store.EndPoint.Tools;
using ZarinPal.Class;

namespace Store.EndPoint.Controllers;

[Authorize]
public class PayController : Controller
{
    private readonly IMediator _mediator;

    private readonly Payment _payment;
    private readonly Authority _authority;
    private readonly Transactions _transactions;
    public PayController(IMediator mediator)
    {
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
        var CustomerCart = await _mediator.Send(new GetCartQuery(userId));
        if (CustomerCart.IsSuccess && CustomerCart.Data.TotalPrice > 0)
        {
            var request = await _mediator.Send(new AddPayRequestCommand(userId, CustomerCart.Data.CartId));
            if (request.IsSuccess)
            {
                var callbackUrl = Url.ActionLink("VerifyPayment", "Pay", new { CustomerPaymentId = request.Data.RequestPayId });
                #region ZarinPal Codes
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = request.Data.PhoneNumber,
                    CallbackUrl = callbackUrl,
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
        var requestDetail = await _mediator.Send(new ValidRequestPayQuery(CustomerPaymentId));// get price to check payment

        #region ZarinPal Codes
        //check payment
        var verification = await _payment.Verification(new DtoVerification
        {
            Amount = requestDetail.Data,
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            Authority = authority
        }, Payment.Mode.sandbox);
        #endregion

        var command = new EditRequestPayCommand(CustomerPaymentId, verification.RefId, authority); //edit payment status and other info

        switch (verification.Status)
        {
            case 100:
                command.IsPay = true;
                await _mediator.Send(new AddOrderCommand(CustomerPaymentId));
                break;
            default:
                command.IsPay = false;
                break;
        }
        await _mediator.Send(command);

        if (status.ToLower() == "ok")
        {
            return RedirectToAction("Index", "Order");
        }
        return RedirectToAction("Index", "Cart");
    }
}
