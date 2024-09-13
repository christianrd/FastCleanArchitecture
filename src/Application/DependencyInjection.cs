using FastCleanArchitecture.Application.Common.Behaviors;
using FastCleanArchitecture.Application.Common.Mappings;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FastCleanArchitecture.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        });

        services.AddMapster();
        MapsterConfig.Configure();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
