using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Orders.Models;
using OrdersWeb.Api.Orders.Queries;
using OrdersWeb.Api.Orders.Repositories;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Orders.Fixtures;
using OrdersWeb.Test.Products.Fixtures;
using OrdersWeb.Test.Startup;

namespace OrdersWeb.Test.Orders.Commands
{
    public class UpdateOrderCommandHandlerShould
    {
        private SetupFixture _setupFixture;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private UpdateOrderCommandHandler _updateOrderCommandHandler;
        private GetOrderByNumberQueryHandler _getOrderByNumberQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _setupFixture = new SetupFixture();
            var connection = _setupFixture.GetConnection();
            _setupFixture.CreateSeed();
            _orderRepository = new OrderRepository(connection);
            _productRepository = new ProductRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _updateOrderCommandHandler = new UpdateOrderCommandHandler(_orderRepository, _mapper);
            _getOrderByNumberQueryHandler =
                new GetOrderByNumberQueryHandler(_orderRepository, _productRepository, _mapper);
        }

        [Test]
        public async Task DisplayNewOrderInformationWhenUpdatedOrder()
        {
            GivenAPostOrder();

            await WhenOrderIsUpdated();

            await ThenShowUpdatedOrderInformation();
        }

        [Test]
        public async Task RetrieveOrderWithProductsAdded()
        {
            var givenOrder = OrderMother.ACustomerAsCustomerWithTwoProducts();

            var products = new List<Product>
            {
                ProductMother.ComputerMonitorAsProduct(),
                ProductMother.KeyboardAsProduct()
            };
            var expectedOrder = new OrderReadDto("ORD765190", "A customer", "An Address", products);
            await _orderRepository.Add(givenOrder);

            var query = new GetOrderByNumberQuery(givenOrder.Number);
            var result = await _getOrderByNumberQueryHandler.Handle(query, default);

            result.Should().BeEquivalentTo(expectedOrder);
        }

        private async Task ThenShowUpdatedOrderInformation()
        {
            var expectedOrder = new OrderReadDto("ORD765190", "New John Doe", "A new Simple Street, 123", new List<Product>());
            var query = new GetOrderByNumberQuery(expectedOrder.Number);
            var result = await _getOrderByNumberQueryHandler.Handle(query, default);
            result.Should().BeEquivalentTo(expectedOrder);
        }

        private async Task WhenOrderIsUpdated()
        {
            var givenUpdateOrder = new OrderUpdateDto(Id: 1, Number: "ORD765190", Customer: "New John Doe",
                Address: "A new Simple Street, 123", new List<int>());

            var order = new Order
            {
                Id = 1,
                Number = "ORD765190",
                Customer = "New John Doe",
                Address = "A new Simple Street, 123",
            };
            _mapper.Map<Order>(givenUpdateOrder).Returns(order);
            var command = new UpdateOrderCommand(givenUpdateOrder);
            await _updateOrderCommandHandler.Handle(command, default);
        }

        private void GivenAPostOrder()
        {
            var order = OrderMother.JohnDoeAsCustomer();
            _orderRepository.Add(order);
        }
    }
}
