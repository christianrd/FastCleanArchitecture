using FastCleanArchitecture.Application;
using FastCleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
#if (UseController)
builder.Services.AddControllers();
#endif

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#if (UseController)
app.MapControllers();
#endif

await app.UseInfrastructureAsync();

app.Run();
