using System.Data.SQLite;

namespace OrderProcessingSystem
{
    public class SQLiteManager
    {
        // Store order in the relational database
        private readonly string _connectionString;

        public SQLiteManager()
        {
            _connectionString = "Data Source = orderProcessingSystem.db; Version = 3;";
        }

        public async Task CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Orders'";
                    var result = await command.ExecuteScalarAsync();

                    if (result == null)
                    {
                        command.CommandText = @"CREATE TABLE Orders 
                        ( OrderID INTEGER,
                          ProductName VARCHAR(20),
                          Quantity INTEGER,
                          Price DOUBLE,
                          OrderDate DATETIME)";

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Orders table created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Orders table already exists. Add orders to process.");
                    }
                }
            }
        }

        public async Task<Order> StoreOrder(Order order)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                int lastOrderId = await GetLastOrderID(connection);
                order.OrderID = lastOrderId + 1;

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Orders(OrderID, ProductName, Quantity, Price, OrderDate) " +
                                          "VALUES (@OrderID, @ProductName, @Quantity, @Price, @OrderDate)";

                    command.Parameters.AddWithValue("@OrderID", order.OrderID);
                    command.Parameters.AddWithValue("@ProductName", order.ProductName);
                    command.Parameters.AddWithValue("@Quantity", order.Quantity);
                    command.Parameters.AddWithValue("@Price", order.Price);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);

                    await command.ExecuteNonQueryAsync();
                }
                order.IsProcessed = true;
                return order;
            }
        }

        private static async Task<int> GetLastOrderID(SQLiteConnection connection)
        {
            int lastOrderID = 0;
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders";
                var result = await command.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    lastOrderID = Convert.ToInt32(result);
                }
            }
            return lastOrderID;
        }
    }
}
