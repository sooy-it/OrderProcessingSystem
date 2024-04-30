using System.Data.SQLite;

namespace OrderProcessingSystem
{
    public class SQLiteManager
    {
        // Store order in the relational database
        private string _connectionString;

        public SQLiteManager()
        {
            _connectionString = "Data Source = orderProcessingSystem.db; Version = 3;";
        }

        public void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Orders'";
                    object result = command.ExecuteScalar();
                    if (result == null)
                    {
                        command.CommandText = @"CREATE TABLE Orders 
                        ( OrderID INTEGER,
                          ProductName VARCHAR(20),
                          Quantity INTEGER,
                          Price DOUBLE,
                          OrderDate DATETIME)";
                    }
                    command.ExecuteNonQuery();
                    Console.WriteLine("Orders table created successfully.");

                }
            }
        }

        public void StoreOrder(Order order)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                int lastOrderId = GetLastOrderID(connection);
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

                    command.ExecuteNonQuery();
                }

                Console.WriteLine($"Order stored successfully. {order.OrderID}");
                order.IsProcessed = true;
            }
        }

        private static int GetLastOrderID(SQLiteConnection connection)
        {
            int lastOrderID = 0;
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(OrderID) FROM Orders";
                var result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    lastOrderID = Convert.ToInt32(result);
                }
            }
            return lastOrderID;

        }
    }
}
