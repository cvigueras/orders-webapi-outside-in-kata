using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Start;

namespace OrdersWeb.Test.Orders.Commands
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
            _startupTest.CreateSeed();
            _orderRepository = new OrderRepository(connection);
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateOrderCommandHandler(_orderRepository, _mapper);
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
            var givenOrder = new Order
            {
                Customer = "A customer",
                Number = "ORD765190",
                Address = "An Address",
                Products = new List<Product>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Computer Monitor",
                        Price = "100€",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Keyboard",
                        Price = "30€",
                    }
                }
            };

            var products = new List<Product>
            {
                new()
                {
                    Id = 1,
                    Name = "Computer Monitor",
                    Price = "100€",
                },
                new()
                {
                    Id = 2,
                    Name = "Keyboard",
                    Price = "30€",
                }
            };
            var expectedOrder = new OrderReadDto("ORD765190", "A customer", "An Address", products);
            await _orderRepository.Add(givenOrder);

            var result = await _orderRepository.GetByOrderNumber(givenOrder.Number);

            result.Should().BeEquivalentTo(expectedOrder);
        }

        private async Task ThenShowUpdatedOrderInformation()
        {
            var expectedOrder = new OrderReadDto("ORD765190", "New John Doe", "A new Simple Street, 123", new List<Product>());
            var result = await _orderRepository.GetByOrderNumber(expectedOrder.Number);
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
            await _handler.Handle(command, default);
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
