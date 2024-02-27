using Cart.Application.Interfaces;
using Cart.Application.KafkaConfiguration;
using Cart.Application.Services;
using Cart.Infrastructure.Repositories;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// discovery
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IHealthCheckHandler, ScopedEurekaHealthCheckHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICartRepository, CartRepository>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IConsumer, ConsumerService>();
builder.Services.Configure<KafkaConsumerConfig>(builder.Configuration.GetSection("KafkaConsumer"));

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

app.UseDiscoveryClient();
app.UseHealthChecks("/info");

var kafkaConsumers = app.Services.GetRequiredService<IConsumer>();
kafkaConsumers.RunInBackground();

app.Run();
