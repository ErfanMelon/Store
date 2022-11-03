using Store.Application.Services.Fainances.Commands.AddPayRequest;
using Store.Application.Services.Fainances.Commands.EditPayRequset;
using Store.Application.Services.Fainances.Queries.GetRequestPays;
using Store.Application.Services.Fainances.Queries.VaildateRequestPay;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IRequestPayFacade
    {
        public IAddPayRequestService addPayRequestService { get; }
        public IValidateRequestPayService validateRequestPayService { get; }
        public IEditRequsetPayService editRequsetPayService { get; }
        public IGetRequestPaysService getRequestPaysService { get; }
    }
}
