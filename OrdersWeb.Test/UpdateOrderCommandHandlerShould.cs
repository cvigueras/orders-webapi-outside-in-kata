using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Commands;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class UpdateOrderCommandHandlerShould
    {
        private StartupTest _startupTest;
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private UpdateOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _startupTest = new StartupTest();
            var connection = _startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateOrderCommandHandler(_orderRepository, _mapper);
        }

        [Test]
        public async Task DisplayNewOrderInformationWhenUpdatedOrder()
        {
            GivenAPostOrder();

            var updateOrder = await WhenOrderIsUpdated();

            ThenShowUpdatedOrderInformation(updateOrder);
        }

        private void ThenShowUpdatedOrderInformation(OrderUpdateDto orderUpdateDto)
        {
            var result = _orderRepository.GetByOrderNumber(orderUpdateDto.Number);
            result.Result.Should().BeEquivalentTo(orderUpdateDto);
        }

        private async Task<OrderUpdateDto> WhenOrderIsUpdated()
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
            var command = new UpdateOrderCommand(givenUpdateOrder);
            await _handler.Handle(command, default);
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
