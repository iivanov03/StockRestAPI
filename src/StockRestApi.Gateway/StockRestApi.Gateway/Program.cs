using StockRestApi.Gateway.Middleware;
using StockRestApi.Gateway.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ProxyApiService, ProxyApiServiceImpl>();

builder.Services.AddTransient<GatewayMiddleware>();
builder.Services.AddScoped<ProxyApiService, ProxyApiServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Add our custom middlewares.
app.UseMiddleware<GatewayMiddleware>();

app.MapControllers();

app.Run();
