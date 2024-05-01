namespace OrderProcessingSystem
{
    public class OrderGenerator
    {
        private readonly OrderProcessor _orderProcessor;
        private readonly Random _random;

        public OrderGenerator(OrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor ?? throw new ArgumentNullException(nameof(orderProcessor));
            _random = new Random();
        }

        //generates a configured number of orders and send it to order processor
        public async Task GenerateOrder(int numberOfOrders)
        {
            for (int i = 0; i < numberOfOrders; i++)
            {
                // Generate random orders
                string productName = "Product" + _random.Next(1, 100);
                int quantity = _random.Next(1, 10);
                double price = Math.Round(_random.NextDouble() * 100, 2);
                DateTime orderDate = DateTime.Now;

                // Create order
                Order order = new Order(productName, quantity, price, orderDate);

                // Process and store order
                if (!order.IsProcessed)
                {
                    await _orderProcessor.ProcessOrder(order);
                }
            }
        }
    }
}
