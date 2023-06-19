using Microsoft.Azure.ServiceBus;
using Sabe.Common.bus;
using Sabe.Inventory.Api.Events;
using Sabe.Inventory.Api.Events.Handlers;
using System.Text.Json;
using System.Text;
namespace Sabe.Inventory.Api.Config
{
    public static class EventBusConsumer
    {
        public static void UseEventHandler(this IApplicationBuilder app)
        {
            var receiver = app.ApplicationServices.GetService<IServiceBus>();

            // Handlers
            var productStockEventHandler = app.ApplicationServices.GetService<IHandler<IEnumerable<ProductInventoryEvent>>>();

            Register(receiver, "INVENTORY-STOCK", productStockEventHandler);
        }

        private static void Register<T>(
            IServiceBus service,
            string queue,
            IHandler<T> handler) where T : class
        {
            var client = service.GetQueueClient(queue);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            client.RegisterMessageHandler(async (Message message, CancellationToken token) => {
                var payload = JsonSerializer.Deserialize<T>(
                    Encoding.UTF8.GetString(message.Body)
                );

                await client.CompleteAsync(message.SystemProperties.LockToken);
                await handler.Execute(payload);
            }, messageHandlerOptions);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            // your custom message log
            return Task.CompletedTask;
        }
    }
}
