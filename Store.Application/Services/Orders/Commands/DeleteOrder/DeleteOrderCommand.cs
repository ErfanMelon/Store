using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.Commands.DeleteOrder;
public class DeleteOrderCommand : IRequest<ResultDto>
{
    public long OrderId { get; set; }
    public class Handler : IRequestHandler<DeleteOrderCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(d => d.OrderDetails)
                .SingleOrDefaultAsync(o => o.OrderId == request.OrderId);

            if (order is null)
                throw new ArgumentNullException("پیدا نشد");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, "پاک شد"); 
        }
    }
}