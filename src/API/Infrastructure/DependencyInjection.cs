#if (UseMinimalApis)
using System.Reflection;
#endif
using Azure.Identity;

namespace FastCleanArchitecture.API.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
#if UseController
        services.AddControllers();
#endif
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
#if (!UseController)
        services
            .AddApiVersioningConfiguration()
            .AddApiBehaviorConfiguration();
#endif
        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["AZURE_KEY_VAULT_ENDPOINT"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
#if (UseMinimalApis)
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();
        
        return app
            .MapGroup("api/v{version:apiVersion}/" + groupName)
            .WithApiVersionSet(apiVersionSet)
            .WithGroupName(groupName)
            .WithTags(groupName);
    }

    public static WebApplication MapEndPoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);
        var assembly = Assembly.GetExecutingAssembly();
        
        var endpointGroupTypes = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                instance.Map(app);
        }

        return app;
    }
#endif
}
