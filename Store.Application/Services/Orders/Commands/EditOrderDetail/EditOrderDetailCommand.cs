using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.Commands.EditOrderDetail;
public class EditOrderDetailCommand : IRequest<ResultDto>
{
    public long OrderDetailId { get; set; }
    public short Count { get; set; }
    public string? Description { get; set; }

    public class Validator : AbstractValidator<EditOrderDetailCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Count).GreaterThanOrEqualTo((short)0);
        }
    }
    public class Handler : IRequestHandler<EditOrderDetailCommand, ResultDto>
    {
        private readonly IDataBaseContext _context;
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Handle(EditOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderdetail = await _context.OrderDetails
                .Include(o => o.Order)
                .ThenInclude(p => p.RequestPay)
                .SingleOrDefaultAsync(d => d.OrderDetailId == request.OrderDetailId);
            if (orderdetail is null)
                throw new ArgumentNullException("سفارش یافت نشد !");

            orderdetail.MoreDetail = request.Description;
            if (orderdetail.Count != request.Count && request.Count >= 0) // customer change products count (reduce just!) and Refund must compute
            {
                orderdetail.Count = request.Count;
                int orderRefund = orderdetail.Order.OrderRefund;// old refund
                int totalPaid = orderdetail.Order.RequestPay.Price;// customer's order's total

                int newTotal = orderdetail.Order.OrderDetails.Sum(d => d.Count * d.Amount);

                orderdetail.Order.OrderRefund = orderRefund + (totalPaid - newTotal);// new refund

                //int totalspend = orderdetail.Count * orderdetail.Amount;
                //orderdetail.Count = request.Count;
                //orderdetail.ProductRefund = totalspend - (orderdetail.Count * orderdetail.Amount);
                //orderdetail.Order.OrderRefund += orderdetail.ProductRefund;
                //orderdetail.Order.UpdateTime = DateTime.Now;
                //orderdetail.UpdateTime = DateTime.Now;
                //if (orderdetail.Order.OrderRefund < 0 || orderdetail.ProductRefund < 0)
                //{
                //    return new ResultDto { Message = "کاربر بدهکار میباشد" };
                //}
            }


            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto(true, "ویرایش با موفقیت انجام شد !");

        }
    }
}