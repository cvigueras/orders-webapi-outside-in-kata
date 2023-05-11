using Dapper;
using FluentAssertions;
using OrdersWeb.Api;
using System.Data.SQLite;

namespace OrdersWeb.Test
{
    public class OrderRepositoryShould
    {
        private SQLiteConnection? connection;

        [SetUp]
        public void SetUp()
        {
            var startupTest = new StartupTest();
            connection = startupTest.GetConnection();
        }

        [Test]
        public void RetrieveEmptyOrderWhenNotExists()
        {
            var orderRepository = new OrderRepository(connection);

            var action = () => orderRepository.GetByOrderNumber("ORD000000").Result;

            action.Should().Throw<InvalidOperationException>();
        }

    }
}
