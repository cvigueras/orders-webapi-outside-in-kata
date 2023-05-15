using Dapper;
using System.Data.SQLite;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api
{
    public class Connection
    {
        public static void CreateDataBase()
        {
            var connection = new SQLiteConnection("Data Source=./Orders.db");

            connection.Execute(@"CREATE TABLE IF NOT EXISTS Orders(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer VARCHAR(200) NOT NULL,
                Address VARCHAR(400) NOT NULL,
                Number VARCHAR(10) NOT NULL)"
            );

            connection.Execute(@"CREATE TABLE IF NOT EXISTS Products(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name VARCHAR(200) NOT NULL,
                Price VARCHAR(20) NOT NULL)"
            );

            if (!ExistTableProducts(connection))
            {
                CreateSeed(connection);
            }

            connection.Close();
            connection.Dispose();
        }

        private static void CreateSeed(SQLiteConnection connection)
        {
            var productList = new List<Product>
                {
                    new()
                    {
                        Name = "Computer Monitor",
                        Price = "100€",
                    },
                    new()
                    {
                        Name = "Keyboard",
                        Price = "30€",
                    },
                    new()
                    {
                        Name = "Mouse",
                        Price = "15€",
                    },
                    new()
                    {
                        Name = "Router",
                        Price = "70€",
                    },
                };

                connection.Execute($"INSERT INTO Products(Name, Price) " +
                                   $"VALUES(@Name, @Price)", productList);
        }

        private static bool ExistTableProducts(SQLiteConnection connection)
        {
            var exist = connection.Query<dynamic>("SELECT COUNT(*) as Count FROM Products");
            return exist.Single().Count > 1;
        }
    }
}
