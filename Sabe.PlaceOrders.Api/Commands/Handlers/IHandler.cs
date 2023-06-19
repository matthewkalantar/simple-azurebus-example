namespace Sabe.PlaceOrders.Api.Commands.Handlers
{
    public interface IHandler<TCommand> where TCommand : class
    {
        public Task Execute(TCommand command);
    }
}
