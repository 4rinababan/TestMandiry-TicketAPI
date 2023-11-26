using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Test_Mandiri;
using Test_Mandiri.Controllers.v1;
using Test_Mandiri.IService;
using Test_Mandiri.Services;

var builder = WebApplication.CreateBuilder(args);

var httpsPort = builder.Configuration.GetValue<int>("HttpsPort", 443);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EfPostgresDb"));

});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["apiVersion"] = typeof(ApiVersionRouteConstraint);
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = httpsPort;
});

// builder.Services.AddApiVersioning(options =>
// {
//     options.ReportApiVersions = true;
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.Conventions.Controller<OrderController>().HasApiVersion(new ApiVersion(1, 0));
// });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITicketService, TicketService>();


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
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
