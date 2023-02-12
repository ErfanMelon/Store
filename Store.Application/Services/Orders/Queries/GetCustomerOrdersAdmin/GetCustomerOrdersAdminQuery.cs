using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Queries.GetCustomerOrdersAdmin;

public class GetCustomerOrdersAdminQuery : IRequest<ResultDto<PaginationDto<GetCustomerOrdersAdminDto>>>
{
    public GetCustomerOrdersAdminQuery(OrderState? state, string? searchKey, int page, int pageSize)
    {
        State = state;
        SearchKey = searchKey;
        Page = page;
        PageSize = pageSize;
    }

    public OrderState? State { get; set; }
    public string? SearchKey { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public class Validator : AbstractValidator<GetCustomerOrdersAdminQuery>
    {
        public Validator()
        {
            RuleFor(e => e.Page).GreaterThan(0);
            RuleFor(e => e.PageSize).GreaterThan(0);
        }
    }
    public class Handler : IRequestHandler<GetCustomerOrdersAdminQuery, ResultDto<PaginationDto<GetCustomerOrdersAdminDto>>>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public Task<ResultDto<PaginationDto<GetCustomerOrdersAdminDto>>> Handle(GetCustomerOrdersAdminQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .AsQueryable();
            if (request.State != null)
            {
                query = query.Where(o => o.OrderState == request.State).AsQueryable();
            }
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(o =>
                o.InsertTime.ToString().Contains(request.SearchKey) ||
                o.PayId.ToString().Contains(request.SearchKey)
                ).AsQueryable();
            }
            var orderlist = query.Select(o => new GetCustomerOrdersAdminDto
            {
                OrderCreation = o.InsertTime,
                OrderId = o.OrderId,
                OrderState = EnumHelpers<OrderState>.GetDisplayValue(o.OrderState),
                PaidPrice = o.RequestPay.Price,
                UserId = o.UserId,
                UserName = o.User.UserFullName,
                Description = o.Description

            }).ToPaged(request. Page,request. PageSize);
            if (!orderlist.Items.Any())
                throw new Exception("هیچ سفارشی پیدا نشد");
            return Task.FromResult(new ResultDto<PaginationDto<GetCustomerOrdersAdminDto>>(orderlist));
        }
    }
}
public class GetCustomerOrdersAdminDto
{
    public long OrderId { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string OrderState { get; set; }
    public DateTime OrderCreation { get; set; }
    public int PaidPrice { get; set; }
    public string? Description { get; set; }
}

