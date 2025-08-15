using GymGo.Api.Exceptions;
using GymGo.Api.Extensions;
using GymGo.Application.Extensions;
using GymGo.Domain.Entities;
using GymGo.Identity.Extensions;
using GymGo.Infrastructure.DataSeeding;
using GymGo.Infrastructure.Extensions;
using GymGo.Persistence.Extensions;
using GymGo.Shared.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInConfigurePersistenceServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GymGo.Api",
        Version = "2.0"
    });
});

builder.Host.AddFileLogger(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();
using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ICsvDataSeeder>();
await seeder.SeedAsync<MembershipType>("SeedData/membershiptypes.csv");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
