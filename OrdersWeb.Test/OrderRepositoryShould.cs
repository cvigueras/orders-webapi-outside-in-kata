using AutoFixture;
using FluentAssertions;
using OrdersWeb.Api;
using OrdersWeb.Api.Models;
using System.Data.SQLite;

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
            var givenOrder = new Order
            {
                Number = "ORD445190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            var lastId = orderRepository.Add(givenOrder);
            var expectedOrder = new Order
            {
                Id = lastId.Result,
                Address = "New Address",
                Customer = "New customer",
                Number = givenOrder.Number,
            };
            orderRepository.Update(expectedOrder);

            var result = orderRepository.GetByOrderNumber(expectedOrder.Number);

            result.Result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}
