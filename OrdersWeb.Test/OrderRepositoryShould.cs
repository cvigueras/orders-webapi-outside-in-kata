using Dapper;
using FluentAssertions;
using OrdersWeb.Api;
using System.Data.SQLite;

namespace OrdersWeb.Test
{
    public class OrderRepositoryShould
    {
        [Test]
        public void RetrieveEmptyOrderWhenNotExists()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();
            connection.Execute(@"CREATE TABLE IF NOT EXISTS Orders(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer VARCHAR(200) NOT NULL,
                Address VARCHAR(400) NOT NULL,
                Number VARCHAR(10) NOT NULL)"
            );
            var orderRepository = new OrderRepository(connection);

            var action = () => orderRepository.GetByOrderNumber("ORD000000").Result;

            action.Should().Throw<InvalidOperationException>();
        }

    }
}
