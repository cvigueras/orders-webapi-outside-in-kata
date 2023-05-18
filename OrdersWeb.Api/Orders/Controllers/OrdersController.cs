using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Orders.Commands;
using OrdersWeb.Api.Orders.Models;
using OrdersWeb.Api.Orders.Queries;

namespace OrdersWeb.Api.Orders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public OrdersController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderCreateDto orderCreateDto)
    {
        var createOrderCommand = new CreateOrderCommand(orderCreateDto);
        return Ok(await _sender.Send(createOrderCommand));
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> Get(string number)
    {
        var query = new GetOrderByNumberQuery(number);
        var order = await _sender.Send(query);
        return Ok(_mapper.Map<OrderReadDto>(order));
    }

    [HttpPut("{number}")]
    public async Task<IActionResult> Put(string number, OrderUpdateDto orderUpdate)
    {
        if (number != orderUpdate.Number)
            return BadRequest("Order number mismatch");
        var query = new UpdateOrderCommand(orderUpdate);
        await _sender.Send(query);
        return Ok("Order updated successfully!");
    }
}