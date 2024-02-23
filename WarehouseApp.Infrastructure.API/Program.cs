using WarehouseApp.Application.Interfaces;
using WarehouseApp.Application.Services;
using WarehouseApp.Domain.Interfaces;
using WarehouseApp.Domain.Services;
using WarrehouseApp.Infrastructure.Data.Interfaces.Redis;
using WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter;
using WarrehouseApp.Infrastructure.Data.Repositories.Redis;
using WarrehouseApp.Infrastructure.Data.Services.Data;
using WarrehouseApp.Infrastructure.Services.SquarePrinter;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
string redisConnectionString = "localhost";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod(); ;
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISquareService, SquareService>();
builder.Services.AddScoped<IApiSquarePrinter, ApiSquarePrinter>();
builder.Services.AddScoped<ICalcShortestDistanceService, CalcShortestDistanceService>();
builder.Services.AddScoped(typeof(IDataService<>), typeof(RedisDataService<>));
builder.Services.AddSingleton<IRedisRepository>(new RedisRepository(redisConnectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
