using AutoFixture;
using FluentAssertions;
using OrdersWeb.Api.Orders;
using OrdersWeb.Test.Orders.Fixtures;
using System.Data.SQLite;
using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders
{
    public class OrderRepositoryShould
    {
        private SQLiteConnection? connection;
        private Fixture fixture;
        private OrderRepository _orderRepository;

        [SetUp]
        public void SetUp()
        {
            var startupTest = new SetupFixture();
            connection = startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            fixture = new Fixture();
        }

        [Test]
        public void RetrieveEmptyOrderWhenNotExists()
        {
            var action = () => _orderRepository.GetByOrderNumber("ORD000000").Result;

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public async Task RetrieveAnExistingOrder()
        {
            var order = fixture.Create<Order>();
            await _orderRepository.Add(order);

            var result = await _orderRepository.GetByOrderNumber(order.Number);

            result.Should().BeEquivalentTo(order);
        }

        [Test]
        public async Task UpdateAnOrder()
        {
            var expectedOrder = await GivenAnUpdatedOrder();

            var result = await _orderRepository.GetByOrderNumber(expectedOrder.Number);

            result.Should().BeEquivalentTo(expectedOrder);
        }

        private async Task<Order> GivenAnUpdatedOrder()
        {
            var givenOrder = OrderMother.JohnDoeAsCustomer();
            var lastId = await _orderRepository.Add(givenOrder);
            var expectedOrder = OrderMother.NewCustomerAsCustomer(lastId, givenOrder);
            await _orderRepository.Update(expectedOrder);
            return expectedOrder;
        }
    }
}