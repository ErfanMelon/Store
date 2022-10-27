using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Orders.Commands.AddOrder;
using Store.Application.Services.Orders.Commands.ChangeOrderDetailState;
using Store.Application.Services.Orders.Commands.ChangeOrderState;
using Store.Application.Services.Orders.Queries.GetCustomerOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrderAdmin;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using Store.Application.Services.Orders.Queries.GetCustomerOrdersAdmin;

namespace Store.Application.Services.Orders.Facade
{
    public class OrderFacade : IOrderFacade
    {
        private readonly IDataBaseContext _context;
        public OrderFacade(IDataBaseContext context)
        {
            _context = context;
        }
        private IAddOrderService _addOrderService;
        public IAddOrderService addOrderService
        {
            get
            {
                return _addOrderService = _addOrderService ?? new AddOrderService(_context);
            }
        }
        private IGetCustomerOrdersService _getCustomerOrdersService;
        public IGetCustomerOrdersService getCustomerOrdersService
        {
            get
            {
                return _getCustomerOrdersService = _getCustomerOrdersService ?? new GetCustomerOrdersService(_context);
            }
        }
        private IGetCustomerOrderService _getCustomerOrderService;
        public IGetCustomerOrderService getCustomerOrderService
        {
            get
            {
                return _getCustomerOrderService = _getCustomerOrderService ?? new GetCustomerOrderService(_context);
            }
        }
        private IGetCustomerOrdersAdminService _getCustomerOrdersAdminService;
        public IGetCustomerOrdersAdminService getCustomerOrdersAdminService
        {
            get
            {
                return _getCustomerOrdersAdminService = _getCustomerOrdersAdminService ?? new GetCustomerOrdersAdminService(_context);
            }
        }
        private IGetCustomerOrderAdminService _getCustomerOrderAdminService;
        public IGetCustomerOrderAdminService getCustomerOrderAdminService
        {
            get
            {
                return _getCustomerOrderAdminService = _getCustomerOrderAdminService ?? new GetCustomerOrderAdminService(_context);
            }
        }
        private IChangeOrderStateService _changeOrderStateService;
        public IChangeOrderStateService changeOrderStateService
        {
            get
            {
                return _changeOrderStateService = _changeOrderStateService ?? new ChangeOrderStateService(_context);
            }
        }
        private IChangeOrderDetailStateService _changeOrderDetailStateService;
        public IChangeOrderDetailStateService changeOrderDetailStateService
        {
            get
            {
                return _changeOrderDetailStateService = _changeOrderDetailStateService ?? new ChangeOrderDetailStateService(_context);
            }
        }
    }
}
