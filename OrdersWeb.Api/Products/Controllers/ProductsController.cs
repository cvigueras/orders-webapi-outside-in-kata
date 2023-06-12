using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Commands;
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
        return await _sender.Send(new GetAllProductsListQuery());
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
    {
        try
        {
            return Ok(await _sender.Send(new CreateProductCommand(productCreateDto)));
        }
        catch (ArgumentException ae)
        {
            return Conflict(ae.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id));
        return product == null ? NotFound() : Ok(product);
    }
}