using Confluent.Kafka;
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

builder.Services.AddSingleton(AutoMapperConfig.Initialize());

builder.Services.AddSingleton<IConsumer<Null, int>>(new ConsumerBuilder<Null, int>(new ConsumerConfig()
    {
        BootstrapServers = "kafka:29092",
        GroupId = "Consumers",
        AutoOffsetReset = AutoOffsetReset.Earliest
    }).Build());
builder.Services.AddSingleton<IProducer<Null, int>>(new ProducerBuilder<Null, int>(new ProducerConfig()
{
    BootstrapServers = "kafka:29092"
}).Build());


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
