using Dapper;
using System.Data.SQLite;

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

            connection.Close();
            connection.Dispose();
        }
    }
}
