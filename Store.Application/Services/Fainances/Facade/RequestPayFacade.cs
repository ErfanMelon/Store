using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Fainances.Commands.AddPayRequest;
using Store.Application.Services.Fainances.Commands.DeletePayRequest;
using Store.Application.Services.Fainances.Commands.EditPayRequset;
using Store.Application.Services.Fainances.Queries.GetRequestPays;
using Store.Application.Services.Fainances.Queries.VaildateRequestPay;

namespace Store.Application.Services.Fainances.Facade
{
    public class RequestPayFacade:IRequestPayFacade
    {
        private readonly IDataBaseContext _context;
        public RequestPayFacade(IDataBaseContext context)
        {
            _context = context;
        }
        private IAddPayRequestService _addPayRequestService;
        public IAddPayRequestService addPayRequestService
        {
            get
            {
                return _addPayRequestService = _addPayRequestService ?? new AddPayRequestService(_context);
            }
        }
        private IValidateRequestPayService _validateRequestPayService;
        public IValidateRequestPayService validateRequestPayService
        {
            get
            {
                return _validateRequestPayService = _validateRequestPayService ?? new ValidateRequestPayService(_context);
            }
        }
        private IEditRequsetPayService _editRequsetPayService;
        public IEditRequsetPayService editRequsetPayService
        {
            get
            {
                return _editRequsetPayService = _editRequsetPayService ?? new EditRequsetPayService(_context);
            }
        }
        private IGetRequestPaysService _getRequestPaysService;
        public IGetRequestPaysService getRequestPaysService
        {
            get
            {
                return _getRequestPaysService = _getRequestPaysService ?? new GetRequestPaysService(_context);
            }
        }
        private IDeletePayRequestService _deletePayRequestService;
        public IDeletePayRequestService deletePayRequestService
        {
            get
            {
                return _deletePayRequestService = _deletePayRequestService ?? new DeletePayRequestService(_context);
            }
        }
    }
}
