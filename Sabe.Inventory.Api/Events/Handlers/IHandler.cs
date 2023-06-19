namespace Sabe.Inventory.Api.Events.Handlers
{
    public interface IHandler<TEvent> where TEvent : class
    {
        Task Execute(TEvent @event);
    }
}
