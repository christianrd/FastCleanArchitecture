using FastCleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Infrastructure;

[TestFixture]
internal abstract class BaseIntegrationTest
{
    private IntegrationTestWebAppFactory _factory;
    private IServiceScope _scope;
    protected ISender Sender;
    protected ApplicationDbContext DbContext;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _factory = new IntegrationTestWebAppFactory();
        await _factory.InitializeAsync();

        _scope = _factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.DisposeAsync();
        _scope.Dispose();
        DbContext.Dispose();
    }

    protected async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        var context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    protected async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        var context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}
