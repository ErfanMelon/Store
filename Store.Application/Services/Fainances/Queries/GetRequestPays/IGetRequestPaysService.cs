using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using System.Linq;

namespace Store.Application.Services.Fainances.Queries.GetRequestPays
{
    public interface IGetRequestPaysService
    {
        ResultDto<RequestPayDto> Execute(int page, int pagesize);
    }
    public class GetRequestPaysService : IGetRequestPaysService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPaysService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<RequestPayDto> Execute(int page, int pagesize)
        {
            var requestpays = _context.RequestPays
                .Include(r=>r.User)
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
                    UserName=r.User.UserFullName
                }).ToPaged(page, pagesize, out int rows);
            return new ResultDto<RequestPayDto>
            {
                Data = new RequestPayDto
                {
                    Pays = requestpays.OrderByDescending(p=>p.PayDate).ToList(),
                    CurrentPage = page,
                    PageSize = pagesize,
                    RowsCount = rows
                },
                IsSuccess = true
            };
        }
    }
    public class RequestPayDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public List<PayDetailDto> Pays { get; set; }
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
}
