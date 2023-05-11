using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrdersController(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderCreateDto order)
    {
        var orderEntity = _mapper.Map<Order>(order);
        await _orderRepository.Add(orderEntity);
        return Ok("Order created");
    }
}