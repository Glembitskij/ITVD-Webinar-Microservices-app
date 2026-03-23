using Microsoft.EntityFrameworkCore;
using Store.Orders.Api.Data;
using Store.Orders.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient для зв'язку зі StockService
builder.Services.AddHttpClient<IStockClient, StockClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5291/");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var context = services.GetRequiredService<OrdersDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Orders API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();