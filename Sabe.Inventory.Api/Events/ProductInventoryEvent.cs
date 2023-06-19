namespace Sabe.Inventory.Api.Events
{  /// <summary>
   /// Event of Send to queue
   /// </summary>
    public class ProductInventoryEvent
    {
        /// <summary>
        /// Auantity that we want to decrease from stock
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Id of product that we cant to decrease inventory
        /// </summary>
        public int ProductId { get; set; }

    }
}
