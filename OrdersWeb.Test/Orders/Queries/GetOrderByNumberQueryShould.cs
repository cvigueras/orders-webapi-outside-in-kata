using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Queries;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Queris
{
    public class GetOrderByNumberQueryShould
    {
        private SetupFixture _setupFixture;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private GetOrderByNumberQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _setupFixture = new SetupFixture();
            var connection = _setupFixture.GetConnection();
            _orderRepository = new OrderRepository(connection);
            _productRepository = new ProductRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _handler = new GetOrderByNumberQueryHandler(_orderRepository,_productRepository, _mapper);
        }

        [Test]
        public async Task DisplayOrderRequested()
        {
            var givenOrder = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
                Products = new List<Product>()
            };
            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123", new List<Product>());
            _mapper.Map<OrderReadDto>(givenOrder).Returns(expectedOrder);
            await _orderRepository.Add(givenOrder);

            var query = new GetOrderByNumberQuery(givenOrder.Number);
            var result = await _handler.Handle(query, default);

            result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}
