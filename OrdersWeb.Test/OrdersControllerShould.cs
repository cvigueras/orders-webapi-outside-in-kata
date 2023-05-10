﻿using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class OrdersControllerShould
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private OrdersController _ordersController;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _mapper = Substitute.For<IMapper>();
            _ordersController = new OrdersController(_orderRepository, _mapper);
        }

        [Test]
        public void CreateOrderWithBasicData()
        {
            var givenOrder = new OrderCreateDto(OrderNumber: "ORD765190", Customer: "John Doe",
                Address: "A Simple Street, 123");

            var order = new Order
            {
                Number = "ORD765190",
                Customer = "John Doe",
                Address = "A Simple Street, 123",
            };
            _mapper.Map<Order>(givenOrder).Returns(order);
            _ordersController.Post(givenOrder);

            _orderRepository.Received().Add(order);
        }
    }
}