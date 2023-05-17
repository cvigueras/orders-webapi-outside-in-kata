using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Orders.Queries;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Commands
{
    public class CreateOrderCommandHandlerShould
    {
        private StartupTest _startupTest;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private CreateOrderCommandHandler _createOrderCommandHandler;
        private GetOrderByNumberQueryHandler _getOrderByNumberQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _startupTest = new StartupTest();
            var connection = _startupTest.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _productRepository = new ProductRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _createOrderCommandHandler = new CreateOrderCommandHandler(_orderRepository, _mapper);
            _getOrderByNumberQueryHandler =
                new GetOrderByNumberQueryHandler(_orderRepository, _productRepository, _mapper);
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
            await _createOrderCommandHandler.Handle(command, default);
            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", new List<Product>());

            var query = new GetOrderByNumberQuery(order.Number);
            var res = await _getOrderByNumberQueryHandler.Handle(query, default);
            
            res.Should().BeEquivalentTo(expectedOrder);
        }
    }
}