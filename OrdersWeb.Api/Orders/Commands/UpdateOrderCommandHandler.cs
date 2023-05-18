﻿using AutoMapper;
using MediatR;
using OrdersWeb.Api.Orders.Models;
using OrdersWeb.Api.Orders.Repositories;

namespace OrdersWeb.Api.Orders.Commands;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.OrderUpdateDto);
        await _orderRepository.Update(order);
    }
}