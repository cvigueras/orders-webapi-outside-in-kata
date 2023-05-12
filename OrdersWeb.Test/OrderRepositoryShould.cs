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

        [Test]
        public void UpdateAnOrder()
        {
            var orderRepository = new OrderRepository(connection);
            var givenOrder = fixture.Create<Order>();
            orderRepository.Add(givenOrder);
            var expectedOrder = new Order
            {
                Address = "New Address",
                Customer = "New customer",
                Number = givenOrder.Number,
            };
            orderRepository.Update(expectedOrder);

            var result = orderRepository.GetByOrderNumber(expectedOrder.Number);

            result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}
