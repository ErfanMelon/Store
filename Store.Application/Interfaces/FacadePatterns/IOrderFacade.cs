using Store.Application.Services.Orders.Commands.AddOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadePatterns
{
    public interface IOrderFacade
    {
        public IAddOrderService addOrderService { get; }
        public IGetCustomerOrdersService getCustomerOrdersService { get; }
        public IGetCustomerOrderService getCustomerOrderService { get; }
    }
}
