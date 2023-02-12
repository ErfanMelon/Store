using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Queries.GetRequestPays;
public class GetRequestPaysQuery : IRequest<ResultDto<PaginationDto<PayDetailDto>>>
{
    public GetRequestPaysQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public class Handler : IRequestHandler<GetRequestPaysQuery, ResultDto<PaginationDto<PayDetailDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public Task<ResultDto<PaginationDto<PayDetailDto>>> Handle(GetRequestPaysQuery request, CancellationToken cancellationToken)
        {
            var requestpays = _context.RequestPays
                .AsNoTracking()
                .Include(r => r.User)
                .Select(r => new PayDetailDto
                {
                    Authority = r.Authority,
                    IsPay = r.IsPay,
                    PayDate = r.PayDate,
                    PayId = r.PayId,
                    Price = r.Price,
                    RefId = r.RefId,
                    UserId = r.UserId,
                    OrderId = _context.Orders.Where(o => o.PayId == r.PayId).Any() ? _context.Orders.Where(o => o.PayId == r.PayId).First().OrderId : 0l,
                    UserName = r.User.UserFullName
                }).ToPaged(request.Page, request.PageSize);

            return Task.FromResult(new ResultDto<PaginationDto<PayDetailDto>>(requestpays));
        }
    }
}
public class PayDetailDto
{
    public Guid PayId { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; }
    public int Price { get; set; }
    public bool IsPay { get; set; }
    public DateTime? PayDate { get; set; }
    public string Authority { get; set; }
    public long RefId { get; set; } = 0;
    public long OrderId { get; set; }
}
