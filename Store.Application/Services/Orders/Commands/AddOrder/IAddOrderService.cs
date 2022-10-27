using Store.Common.Dto;

namespace Store.Application.Services.Orders.Commands.AddOrder
{
    public interface IAddOrderService
    {
        ResultDto Execute(Guid requestPayId);
    }
}
