namespace Sabe.PlaceOrders.Api.Commands.Impl
{
    public class PlaceOrderCommand : ICommand
    {
        public int CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<OrderItemCreateCommand> Items { get; set; }
    }
}
