using MediatR;
using Microsoft.Extensions.Logging;
using Store.Common.Dto;

namespace Store.Application.Interfaces.Behavior;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) // if an error occur (validation,nullargument,...) this code execute
        {
            _logger.LogError(ex, ex.Message);

            // All The Response of application return one of these type (ResultDto or ResultDto<>)
            TResponse response;
            if (typeof(TResponse) == typeof(ResultDto)) // If Response is ResultDto it creates an object and sends it to user
                response = (TResponse)Convert.ChangeType(new ResultDto(ex.Message), typeof(TResponse));

            else if (typeof(TResponse).Name == typeof(ResultDto<>).Name)// If Response is ResultDto<> first create an genericType and pass it to ResultDto<>  then show error
            {
                var resultDtoGeneric = typeof(ResultDto<>).MakeGenericType(typeof(TResponse).GenericTypeArguments[0]);
                var resultDto = Activator.CreateInstance(resultDtoGeneric, null, false, ex.Message);
                response = (TResponse)Convert.ChangeType(resultDto, typeof(TResponse)); // convert resultdto<T> to itself TResponse => ResultDto<T>
            }
            else // if Response is none of ResultDto and ResultDto<T> , it return null (Never Reach!)
                response = default!;
            return response;
        }
    }
}
