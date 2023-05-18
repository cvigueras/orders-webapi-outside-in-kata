using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Queries;

namespace OrdersWeb.Api.Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        var query = new GetAllProductsListQuery();
        return await _sender.Send(query);
    }
}