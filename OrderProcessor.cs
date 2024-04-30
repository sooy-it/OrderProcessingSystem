namespace OrderProcessingSystem
{
    public class OrderProcessor
    {
        // order processor stores the order in a relational database
        private readonly SQLiteManager _sqliteManager;

        public OrderProcessor(SQLiteManager sqliteManager)
        {
            _sqliteManager = sqliteManager;
        }
        public void ProcessOrder(Order order)
        {
            _sqliteManager.StoreOrder(order);
            if (order.IsProcessed)
            {
                PrintReceipt(order);
            }
            else
            {
                Console.WriteLine($"Failed to process order ID: {order.OrderID}");
            }

        }
        public void PrintReceipt(Order order)
        {
            string receipt = "-----------------------------------\n" +
                         $"Receipt for Order ID: {order.OrderID}\n" +
                         $"Product: {order.ProductName}\n" +
                         $"Quantity: {order.Quantity}\n" +
                         $"Price: {order.Price}\n" +
                         $"Order Date: {order.OrderDate}\n" +
                         $"IsProcessed: {order.IsProcessed}\n" +
                         "-----------------------------------\n";

            Console.WriteLine(receipt);
        }
    }
}
