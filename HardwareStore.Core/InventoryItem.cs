namespace HardwareStore.Core
{
    public class InventoryItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string Supplier { get; set; }
    }
}
