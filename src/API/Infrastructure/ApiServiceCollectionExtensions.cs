using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.API.Infrastructure;

internal static class ApiServiceCollectionExtensions
{
    /// <summary>
    /// Adds service API versioning Configuration to the specified services collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection">services</see> available in the application.</param>
    /// <returns>The original <paramref name="services"/> object.</returns>
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// Adds service API Behavior Configuration to the specified services collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection">services</see> available in the application.</param>
    /// <returns>The original <paramref name="services"/> object.</returns>
    public static IServiceCollection AddApiBehaviorConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressInferBindingSourcesForParameters = true;
        });

        return services;
    }
}
