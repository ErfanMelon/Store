using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Commands.AddPayRequest
{
    public interface IAddPayRequestService
    {
        ResultDto<ResultAddPayRequest> Execute(PayRequestDto request);
    }
}
