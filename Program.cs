namespace OrderProcessingSystem
{
    internal class Program
    {
        //Main method to run the generation and storing by calling GenerateOrder with a number and reference to the OrderProcessor.
        static void Main(string[] args)
        {
            SQLiteManager sqliteManager = new SQLiteManager();
            sqliteManager.CreateTable();
            OrderProcessor orderProcessor = new OrderProcessor(sqliteManager);
            OrderGenerator orderGenerator = new OrderGenerator(orderProcessor);

            Console.WriteLine("Input number of orders to process");
            var input = Console.ReadLine();

            int numberOfOrders = int.TryParse(input, out int result) ? result : 0;
            orderGenerator.GenerateOrder(numberOfOrders);
        }
    }
}
