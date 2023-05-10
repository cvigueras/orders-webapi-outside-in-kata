using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public object Post(OrderReadDto order)
    {
        throw new NotImplementedException();
    }
}