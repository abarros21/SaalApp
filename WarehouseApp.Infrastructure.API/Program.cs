using WarehouseApp.Application.Interfaces;
using WarehouseApp.Application.Services;
using WarehouseApp.Domain.Interfaces;
using WarehouseApp.Domain.Services;
using WarrehouseApp.Infrastructure.Interfaces;
using WarrehouseApp.Infrastructure.Services.SquarePrinter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISquareService, SquareService>();
builder.Services.AddScoped<IApiSquarePrinter, ApiSquarePrinter>();
builder.Services.AddScoped<ICalcShortestDistanceService, CalcShortestDistanceService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
