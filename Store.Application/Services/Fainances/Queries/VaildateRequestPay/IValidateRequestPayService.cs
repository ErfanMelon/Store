using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Queries.VaildateRequestPay
{
    public interface IValidateRequestPayService
    {
        ResultDto<ValidationRequsetPayDto> Execute(Guid RequsetPayId);
    }
    public class ValidateRequestPayService : IValidateRequestPayService
    {
        private readonly IDataBaseContext _context;
        public ValidateRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ValidationRequsetPayDto> Execute(Guid RequsetPayId)
        {
            var requestPay = _context.RequestPays.Find(RequsetPayId);
            if (requestPay != null)
            {
                return new ResultDto<ValidationRequsetPayDto>
                {
                    Data = new ValidationRequsetPayDto
                    {
                        Price = requestPay.Price
                    },
                    IsSuccess = true,
                    Message="درخواست پیدا شد !"
                };
            }
            return new ResultDto<ValidationRequsetPayDto>
            {
                Data = new ValidationRequsetPayDto
                {
                    Price=0
                },
                Message = "درخواستی یافت نشد !"
            };
        }
    }
    public class ValidationRequsetPayDto
    {
        public int Price { get; set; }
    }
}
