using FastCleanArchitecture.Infrastructure.Data;
using MediatR;
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
}
