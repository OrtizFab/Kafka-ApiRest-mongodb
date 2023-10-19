using Confluent.Kafka;
using IntegrationWithKafka.Services;
using HostedServices = Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
///
builder.Services.AddSingleton<HostedServices.IHostedService, PaymentService>();

var producerConfig = new ProducerConfig();
var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("producer", producerConfig);
builder.Configuration.Bind("consumer", consumerConfig);

builder.Services.AddSingleton<ProducerConfig>(producerConfig);
builder.Services.AddSingleton<ConsumerConfig>(consumerConfig);


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
