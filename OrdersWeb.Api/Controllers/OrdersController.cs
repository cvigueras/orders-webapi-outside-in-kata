using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Controllers;

public class GetOrderByNumberQuery
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByNumberQuery(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderReadDto> Handle(string number)
    {
        var order = await _orderRepository.GetByOrderNumber(number);
        return _mapper.Map<OrderReadDto>(order);
    }
}

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly GetOrderByNumberQuery _getOrderByNumberQuery;

    public OrdersController(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _getOrderByNumberQuery = new GetOrderByNumberQuery(orderRepository, _mapper);
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
        return await _getOrderByNumberQuery.Handle(number);
    }

    [HttpPut("{number}")]
    public void Put(string number, OrderUpdateDto orderUpdate)
    {
        var order = _mapper.Map<Order>(orderUpdate);
        _orderRepository.Update(order);
    }
}