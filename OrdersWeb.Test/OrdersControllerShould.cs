using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class OrdersControllerShould
    {
        private IOrderRepository _orderRepository;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
        }

        [Test]
        public void CreateOrderWithBasicData()
        {
            var givenOrder = new OrderReadDto(OrderNumber: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");
            var orderController = new OrderController();

            var result = orderController.Post(givenOrder);
            Order order = new Order
            {
                OrderNumber = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            _orderRepository.Received().Add(order);

            result.Should().BeEquivalentTo(givenOrder);
        }
    }
}
