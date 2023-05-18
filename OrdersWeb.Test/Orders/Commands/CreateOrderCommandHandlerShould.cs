using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Orders.Queries;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Orders.Fixtures;
using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Commands
{
    public class CreateOrderCommandHandlerShould
    {
        private SetupFixture _setupFixture;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private CreateOrderCommandHandler _createOrderCommandHandler;
        private GetOrderByNumberQueryHandler _getOrderByNumberQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _setupFixture = new SetupFixture();
            var connection = _setupFixture.GetConnection();
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
            await GivenAPostedOrderWithZeroProducts();

            var result = await WhenRetrievePostedOrder();

            ThenResultShouldBeExpectedOrder(result);
        }

        private static void ThenResultShouldBeExpectedOrder(Order result)
        {
            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", new List<Product>());
            result.Should().BeEquivalentTo(expectedOrder);
        }

        private async Task<Order> WhenRetrievePostedOrder()
        {
            var query = new GetOrderByNumberQuery("ORD765190");
            var result = await _getOrderByNumberQueryHandler.Handle(query, default);
            return result;
        }

        private async Task GivenAPostedOrderWithZeroProducts()
        {
            var givenCreateOrder = new OrderCreateDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", new List<Product>());

            var order = OrderMother.JohnDoeAsCustomerWithZeroProducts();

            _mapper.Map<Order>(givenCreateOrder).Returns(order);

            var command = new CreateOrderCommand(givenCreateOrder);
            await _createOrderCommandHandler.Handle(command, default);
        }
    }
}