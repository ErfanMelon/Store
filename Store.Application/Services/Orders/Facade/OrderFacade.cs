using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Orders.Commands.AddOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Orders.Facade
{
    public class OrderFacade: IOrderFacade
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
    }
}
