using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Queries;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Queris
{
    public class GetOrderByNumberQueryShould
    {
        private StartupTest _startupTest;
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private GetOrderByNumberQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _startupTest = new StartupTest();
            var connection = _startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _handler = new GetOrderByNumberQueryHandler(_orderRepository, _mapper);
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

            var query = new GetOrderByNumberQuery(givenOrder.Number);
            var result = _handler.Handle(query, default);

            result.Result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}
