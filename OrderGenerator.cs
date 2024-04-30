namespace OrderProcessingSystem
{
    public class OrderGenerator
    {
        private readonly OrderProcessor _orderProcessor;
        private readonly Random random;

        public OrderGenerator(OrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
            random = new Random();
        }

        //generates a configured number of orders and send it to order processor
        public void GenerateOrder(int numberOfOrders)
        {
            for (int i = 0; i < numberOfOrders; i++)
            {
                // Generate random orders
                string productName = "Product" + random.Next(1, 100);
                int quantity = random.Next(1, 10);
                double price = Math.Round(random.NextDouble() * 100, 2);
                DateTime orderDate = DateTime.Now;

                // Create order
                Order order = new Order(productName, quantity, price, orderDate);

                // Process and store order
                if (!order.IsProcessed)
                {
                    _orderProcessor.ProcessOrder(order);
                }
            }
        }
    }
}
