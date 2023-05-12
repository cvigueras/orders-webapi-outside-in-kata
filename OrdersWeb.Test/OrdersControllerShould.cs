using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class OrdersControllerShould
    {
        private StartupTest _startupTest;
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private OrdersController _ordersController;

        [SetUp]
        public void SetUp()
        {
            _startupTest = new StartupTest();
            var connection = _startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _ordersController = new OrdersController(_orderRepository, _mapper);
        }

        [Test]
        public async Task CreateOrderWithBasicData()
        {
            var givenCreateOrder = new OrderCreateDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");
            var order = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            _mapper.Map<Order>(givenCreateOrder).Returns(order);

            _ordersController.Post(givenCreateOrder);

            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");
            var result = await _orderRepository.GetByOrderNumber("ORD765190");
            result.Should().BeEquivalentTo(expectedOrder);
        }

        [Test]
        public void DisplayOrderRequested()
        {
            var givenOrder = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");
            _mapper.Map<OrderReadDto>(givenOrder).Returns(expectedOrder);
            _orderRepository.Add(givenOrder);

            var result = _ordersController.Get("ORD765190");

            result.Result.Should().BeEquivalentTo(expectedOrder);
        }

        [Test]
        public void DisplayNewOrderInformationWhenUpdatedOrder()
        {
            GivenAPostOrder();

            var updateOrder = WhenOrderIsUpdated();

            ThenShowUpdatedOrderInformation(updateOrder);
        }

        private void ThenShowUpdatedOrderInformation(OrderUpdateDto orderUpdateDto)
        {
            var result = _orderRepository.GetByOrderNumber(orderUpdateDto.Number);
            result.Result.Should().BeEquivalentTo(orderUpdateDto);
        }

        private OrderUpdateDto WhenOrderIsUpdated()
        {
            var givenUpdateOrder = new OrderUpdateDto(Id: 1, Number: "ORD765190", Customer: "New John Doe",
                Address: "A new Simple Street, 123");

            var order = new Order
            {
                Id = 1,
                Number = "ORD765190",
                Customer = "New John Doe",
                Address = "A new Simple Street, 123",
            };
            _mapper.Map<Order>(givenUpdateOrder).Returns(order);
            _ordersController.Put(givenUpdateOrder.Number, givenUpdateOrder);
            return givenUpdateOrder;
        }

        private void GivenAPostOrder()
        {
            var order = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            _orderRepository.Add(order);
        }
    }
}
