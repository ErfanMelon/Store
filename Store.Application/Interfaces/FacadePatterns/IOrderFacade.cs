using Store.Application.Services.Orders.Commands.AddOrder;
using Store.Application.Services.Orders.Commands.ChangeOrderDetailState;
using Store.Application.Services.Orders.Commands.ChangeOrderState;
using Store.Application.Services.Orders.Commands.DeleteOrder;
using Store.Application.Services.Orders.Commands.EditOrder;
using Store.Application.Services.Orders.Commands.EditOrderDetail;
using Store.Application.Services.Orders.Queries.GetCustomerOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrderAdmin;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using Store.Application.Services.Orders.Queries.GetCustomerOrdersAdmin;
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
        public IGetCustomerOrdersAdminService getCustomerOrdersAdminService { get; }
        public IGetCustomerOrderAdminService getCustomerOrderAdminService { get; }
        public IChangeOrderStateService changeOrderStateService { get; }
        public IChangeOrderDetailStateService changeOrderDetailStateService { get; }
        public IEditOrderDetailService editOrderDetailService { get; }
        public IEditOrderService editOrderService { get; }
        public IDeleteOrderService deleteOrderService { get; }
    }
}
