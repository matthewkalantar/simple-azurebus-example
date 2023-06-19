namespace Sabe.Inventory.Api.Events.Handlers
{
    public class ProductInventoryEventHandler : IHandler<IEnumerable<ProductInventoryEvent>>
    {
        public async Task Execute(IEnumerable<ProductInventoryEvent> @event)
        {
            // Do something with  event
        }
    }
}
