using FluentAssertions;
using OrdersWeb.Api;
using OrdersWeb.Api.Models;
using System.Data.SQLite;
using AutoFixture;

namespace OrdersWeb.Test
{
    public class OrderRepositoryShould
    {
        private SQLiteConnection? connection;
        private Fixture fixture;
        [SetUp]
        public void SetUp()
        {
            var startupTest = new StartupTest();
            connection = startupTest.GetConnection();
            fixture = new Fixture();
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
            var order = fixture.Create<Order>();
            await orderRepository.Add(order);

            var result = await orderRepository.GetByOrderNumber(order.Number);

            result.Should().BeEquivalentTo(order);
        }
    }
}
