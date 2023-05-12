using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Controllers;

public class GetOrderByNumberQueryHandler : IRequestHandler<GetOrderByNumberQuery, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByNumberQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }


    public async Task<Order> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByOrderNumber(request.OrderNumber);
    }
}

public class GetOrderByNumberQuery : IRequest<Order>
{
    public GetOrderByNumberQuery(string orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public string OrderNumber { get; set; }
}

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly GetOrderByNumberQueryHandler _getOrderByNumberQueryHandler;

    public OrdersController(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _getOrderByNumberQueryHandler = new GetOrderByNumberQueryHandler(orderRepository, _mapper);
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderCreateDto order)
    {
        var orderEntity = _mapper.Map<Order>(order);
        await _orderRepository.Add(orderEntity);
        //TODO RETURN NUMBER
        return Ok("ORD878878");
    }

    [HttpGet("{number}")]
    public async Task<OrderReadDto> Get(string number)
    {
        var query = new GetOrderByNumberQuery(number);
        var order = await _getOrderByNumberQueryHandler.Handle(query, default);
        return _mapper.Map<OrderReadDto>(order);
    }

    [HttpPut("{number}")]
    public void Put(string number, OrderUpdateDto orderUpdate)
    {
        var order = _mapper.Map<Order>(orderUpdate);
        _orderRepository.Update(order);
    }
}