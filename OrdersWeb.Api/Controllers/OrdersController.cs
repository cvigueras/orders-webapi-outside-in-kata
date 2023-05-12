using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Commands;
using OrdersWeb.Api.Models;
using OrdersWeb.Api.Queries;

namespace OrdersWeb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly GetOrderByNumberQueryHandler _getOrderByNumberQueryHandler;
    private readonly CreateOrderCommandHandler _createOrderCommandHandler;
    private readonly UpdateOrderCommandHandler _updateOrderCommandHandler;

    public OrdersController(IOrderRepository orderRepository, IMapper mapper)
    {
        _mapper = mapper;
        _getOrderByNumberQueryHandler = new GetOrderByNumberQueryHandler(orderRepository, _mapper);
        _createOrderCommandHandler = new CreateOrderCommandHandler(orderRepository, _mapper);
        _updateOrderCommandHandler = new UpdateOrderCommandHandler(orderRepository, _mapper);
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderCreateDto orderCreateDto)
    {
        var createOrderCommand = new CreateOrderCommand(orderCreateDto);
        await _createOrderCommandHandler.Handle(createOrderCommand, default);
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
    public async Task<IActionResult> Put(string number, OrderUpdateDto orderUpdate)
    {
        if (number != orderUpdate.Number)
            return BadRequest("Order number mismatch");
        var query = new UpdateOrderCommand(orderUpdate);
        await _updateOrderCommandHandler.Handle(query, default);
        return Ok("Order updated successfully!");
    }
}