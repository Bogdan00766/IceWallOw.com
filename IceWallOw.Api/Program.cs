using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DbInfrastructure;
using DbInfrastructure.Repositories;
using Domain.IRepositories;
using IceWallOw.Application.Interfaces;
using IceWallOw.Application.Mappings;
using IceWallOw.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IceWallOwDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ILogger, Logger<>>();


builder.Services.AddSingleton(AutoMapperConfig.Initialize());

builder.Services.AddSingleton<IProducer<Null, int>>(new ProducerBuilder<Null, int>(new ProducerConfig()
{
    BootstrapServers = "kafka:29092"
}).Build());
builder.Services.AddSingleton<IConsumer<Null, int>>(x =>
{
    var consumer = new ConsumerBuilder<Null, int>(new ConsumerConfig()
    {
        BootstrapServers = "kafka:29092",
        GroupId = "Consumers",
        AutoOffsetReset = AutoOffsetReset.Earliest,
    }).Build();
    consumer.Subscribe("Tickets");
    return consumer;
});
//kafka setup
using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = "kafka:29092" }).Build())
{
    try
    {
        if (adminClient.GetMetadata(TimeSpan.FromSeconds(5)).Topics.Where(x => x.Topic == "Tickets").FirstOrDefault() == null)
            adminClient.CreateTopicsAsync(new TopicSpecification[]
            {
                new TopicSpecification
                {
                    Name = "Tickets",
                    ReplicationFactor = 1,
                    NumPartitions = 1
                }
            });
    }
    catch (CreateTopicsException e)
    {
        throw e;
    }
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.EnableAnnotations();
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();   
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


