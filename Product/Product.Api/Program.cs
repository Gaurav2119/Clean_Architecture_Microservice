using Product.Application.Interfaces;
using Product.Application.Services;
using Product.Infrastructure.Repositories;
using Product.Application.Extensions;
using Product.Application.KafkaConfiguration;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddControllers();

// discovery
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IHealthCheckHandler, ScopedEurekaHealthCheckHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomJwtAuthentication();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.Configure<KafkaProducerConfig>(builder.Configuration.GetSection("KafkaProducer"));
builder.Services.AddSingleton<IProducer, ProducerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseDiscoveryClient();
app.UseHealthChecks("/info");

app.Run();
