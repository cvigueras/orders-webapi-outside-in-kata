using FluentAssertions;
using OrdersWeb.Api;
using OrdersWeb.Api.Models;
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

        [Test]
        public async Task RetrieveAnExistingOrder()
        {
            var orderRepository = new OrderRepository(connection);
            var order = new Order
            {
                Address = "New Address",
                Customer = "John Doe",
                Number = "ORD987654",
            };
            await orderRepository.Add(order);

            var result = await orderRepository.GetByOrderNumber("ORD987654");

            result.Should().BeEquivalentTo(order);
        }
    }
}
