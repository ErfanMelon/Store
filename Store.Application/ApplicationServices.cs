using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Interfaces.Behavior;
using System.Reflection;

namespace Store.Application;

public static class ApplicationServices
{
    /// <summary>
    /// Application Layer' Pipelines' Requierments
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
