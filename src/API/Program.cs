using FastCleanArchitecture.API.Infrastructure;
using FastCleanArchitecture.Application;
using FastCleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.UseInfrastructureAsync();
#if (UseMinimalApis)
app.MapEndPoints();
#endif
app.Run();

public partial class Program;
