namespace OrderProcessingSystem
{
    public class OrderProcessor
    {
        // order processor stores the order in a relational database
        private readonly SQLiteManager _sqliteManager;

        public OrderProcessor(SQLiteManager sqliteManager)
        {
            _sqliteManager = sqliteManager ?? throw new ArgumentNullException(nameof(sqliteManager));
        }
        public async Task ProcessOrder(Order order)
        {
            var savedOrder = await _sqliteManager.StoreOrder(order);
            if (savedOrder.IsProcessed)
            {
                PrintReceipt(savedOrder);
            }
            else
            {
                Console.WriteLine($"Failed to process order ID: {savedOrder.OrderID}");
            }
        }

        public static void PrintReceipt(Order order)
        {
            string receipt = "-----------------------------------\n" +
                         "             RECEIPT            \n" +
                         $"Order ID: {order.OrderID}\n" +
                         $"Product Name: {order.ProductName}\n" +
                         $"Quantity: {order.Quantity}\n" +
                         $"Price: {order.Price}\n" +
                         $"Order Date: {order.OrderDate}\n" +
                         $"IsProcessed: {order.IsProcessed}\n" +
                         "-----------------------------------\n";

            Console.WriteLine(receipt);
        }
    }
}
