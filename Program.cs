namespace OrderProcessingSystem
{
    public class Program
    {
        //Main method to run the generation and storing by calling GenerateOrder with a number and reference to the OrderProcessor.
        static async Task Main(string[] args)
        {
            SQLiteManager sqliteManager = new SQLiteManager();
            await sqliteManager.CreateTable();

            OrderProcessor orderProcessor = new OrderProcessor(sqliteManager);
            OrderGenerator orderGenerator = new OrderGenerator(orderProcessor);

            Console.WriteLine("Input number of orders to process");
            var input = Console.ReadLine();

            int numberOfOrders = int.TryParse(input, out int result) ? result : 0;

            await orderGenerator.GenerateOrder(numberOfOrders);
        }
    }
}
