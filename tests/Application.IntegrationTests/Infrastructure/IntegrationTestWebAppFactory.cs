using System.Diagnostics;
using FastCleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#if (!UseOracle)

using Testcontainers.MsSql;

#else
using Testcontainers.Oracle;
#endif

namespace Application.IntegrationTests.Infrastructure;

internal class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
#if (UseOracle)
    private readonly OracleContainer _dbcontainer = new OracleBuilder()
            .WithImage("container-registry.oracle.com/database/express:latest")
            .Build();
#else

    private readonly MsSqlContainer _dbcontainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

#endif

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll(typeof(DbContextOptions<ApplicationDbContext>))
                .AddDbContext<ApplicationDbContext>(options =>
                {
#if (UseOracle)
                    options.UseOracle(_dbcontainer.GetConnectionString());
#else
                    options.UseSqlServer(_dbcontainer.GetConnectionString());
#endif
                });
            services.AddScoped(_ => Stopwatch.StartNew());
        });
    }

    public async Task InitializeAsync()
    {
        await _dbcontainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbcontainer.StopAsync();
    }
}
