using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Commands.DeletePayRequest;
public class DeletePayRequestCommand : IRequest<ResultDto>
{
    public DeletePayRequestCommand(Guid payId)
    {
        PayId = payId;
    }

    public Guid PayId { get; set; }
    public class Handler : IRequestHandler<DeletePayRequestCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(DeletePayRequestCommand request, CancellationToken cancellationToken)
        {
            var pay = await _context.RequestPays
                .SingleOrDefaultAsync(p => p.PayId == request.PayId);
            if (pay is null)
                throw new ArgumentNullException("پرداختی پیدا نشد");

            _context.RequestPays.Remove(pay);

            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto(true, "پرداخت پاک شد");

        }
    }
}