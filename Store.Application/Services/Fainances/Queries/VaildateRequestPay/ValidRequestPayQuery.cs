using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Queries.VaildateRequestPay;
public class ValidRequestPayQuery : IRequest<ResultDto<int>>
{
    public ValidRequestPayQuery(Guid payId)
    {
        PayId = payId;
    }

    public Guid PayId { get; set; }
    public class Handler : IRequestHandler<ValidRequestPayQuery, ResultDto<int>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<int>> Handle(ValidRequestPayQuery request, CancellationToken cancellationToken)
        {
            var requestPay = await _context.RequestPays
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.PayId == request.PayId);

            if (requestPay is null)
                throw new ArgumentNullException("پرداخت معتبر نیست");

            return new ResultDto<int>(requestPay.Price);
        }
    }
}