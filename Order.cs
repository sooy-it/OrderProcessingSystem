namespace OrderProcessingSystem
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsProcessed { get; set; }

        public Order(string productName, int quantity, double price, DateTime orderDate)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            OrderDate = orderDate;
            IsProcessed = false;
        }
    }
}
