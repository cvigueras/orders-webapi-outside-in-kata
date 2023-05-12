﻿using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Commands;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
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
                Address: "A Simple Street, 123");
            var order = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            _mapper.Map<Order>(givenCreateOrder).Returns(order);

            var command = new CreateOrderCommand(givenCreateOrder);
            await _handler.Handle(command, default);

            var expectedOrder = new OrderReadDto(Number: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");
            var result = await _orderRepository.GetByOrderNumber("ORD765190");
            result.Should().BeEquivalentTo(expectedOrder);
        }
    }
}