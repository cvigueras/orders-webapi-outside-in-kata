using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Commands
{
    public class CreateOrderCommandHandlerShould
    {
        private StartupTest _startupTest;
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private CreateOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _startupTest = new StartupTest();
            var connection = _startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateOrderCommandHandler(_orderRepository, _mapper);
        }

        [Test]
        public async Task CreateOrderWithBasicData()
        {
            var givenCreateOrder = new OrderCreateDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", null);
            var order = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
                Products = null
            };
            _mapper.Map<Order>(givenCreateOrder).Returns(order);

            var command = new CreateOrderCommand(givenCreateOrder);
            await _handler.Handle(command, default);

            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", null);
            var result = await _orderRepository.GetByOrderNumber("ORD765190");
            result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}