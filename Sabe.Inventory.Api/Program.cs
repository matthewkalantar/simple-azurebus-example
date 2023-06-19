using Sabe.Common.bus;
using Sabe.Inventory.Api.Events;
using Sabe.Inventory.Api.Events.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Receiver bus
builder.Services.AddSingleton<IServiceBus, ServiceBus>();

// Handlers
builder.Services.AddSingleton<IHandler<IEnumerable<ProductInventoryEvent>>, ProductInventoryEventHandler>();
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
