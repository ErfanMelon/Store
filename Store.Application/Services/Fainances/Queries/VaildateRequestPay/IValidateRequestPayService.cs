using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Queries.VaildateRequestPay
{
    public interface IValidateRequestPayService
    {
        ResultDto<ValidationRequsetPayDto> Execute(Guid RequsetPayId);
    }
}
