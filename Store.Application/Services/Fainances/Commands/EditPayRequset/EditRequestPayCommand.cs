using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;

namespace Store.Application.Services.Fainances.Commands.EditPayRequset;
public class EditRequestPayCommand : IRequest
{
    public EditRequestPayCommand(Guid requsetPayId, int refId, string authority)
    {
        RequsetPayId = requsetPayId;
        RefId = refId;
        Authority = authority;
    }

    public bool IsPay { get; set; }
    public Guid RequsetPayId { get; }
    public int RefId { get; }
    public string Authority { get; }
    public class Handler : IRequestHandler<EditRequestPayCommand>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditRequestPayCommand request, CancellationToken cancellationToken)
        {
            var pay = await _context.RequestPays
                .Include(r => r.Cart)
                .SingleOrDefaultAsync(r => r.PayId == request.RequsetPayId);
            if (pay is null)
                throw new ArgumentNullException("پرداختی صورت نگرفته است");

            pay.Authority = request.Authority;
            pay.IsPay = request.IsPay;
            pay.RefId = request.RefId;
            if (request.IsPay)
                pay.Cart.CurrentCart = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}