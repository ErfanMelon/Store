using MediatR;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.Commands.EditOrder;
public class EditOrderCommand : IRequest<ResultDto>
{
    public long OrderId { get; set; }
    public string? Description { get; set; }
    public class Handler : IRequestHandler<EditOrderCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FindAsync(request.OrderId);
            if (order is null)
                throw new ArgumentNullException("سفارش پیدا نشد");

            order.Description = request.Description.Trim();

            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto(true, "با موفقیت ویرایش شد");
        }
    }
}