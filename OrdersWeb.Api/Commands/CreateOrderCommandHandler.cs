﻿using AutoMapper;
using MediatR;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.OrderCreateDto);
        return await _orderRepository.Add(order);
    }
}