using System.Text.Json;
using System.Text;
using Sabe.PlaceOrders.Api.Commands.Impl;
using Microsoft.Azure.ServiceBus;
using Sabe.Common.bus;
using Sabe.PlaceOrders.Api.Events;

namespace Sabe.PlaceOrders.Api.Commands.Handlers
{
    public class PlaceOrderHandler : IHandler<PlaceOrderCommand>
    {
        private readonly IServiceBus _serviceBus;
        public PlaceOrderHandler(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public async Task Execute(PlaceOrderCommand command)
        {
            // 01. Your logic to order creation

            // 02. Azure Service Bus
            var client = _serviceBus.GetQueueClient("INVENTORY-STOCK");

            var json = JsonSerializer.Serialize(
                command.Items.Select(x => new ProductInventoryEvent
                {
                    ProductId = x.ProductId,
                    Qty = x.Quantity
                }).ToList()
            );
            
            // Add To azure queue
            await client.SendAsync(
                new Message(Encoding.UTF8.GetBytes(json))
            );

            await client.CloseAsync();

            //Then in InventoryStock we receive this Event from queue and make changes in Stock
        }
    }
}
