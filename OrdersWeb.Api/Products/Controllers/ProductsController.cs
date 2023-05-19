using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Commands;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Queries;
using OrdersWeb.Api.Products.Repositories;

namespace OrdersWeb.Api.Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender, IProductRepository productRepository, IMapper mapper)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        var query = new GetAllProductsListQuery();
        return await _sender.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
    {
        try
        {
            var query = new CreateProductCommand(productCreateDto);
            return Ok(await _sender.Send(query));
        }
        catch (ArgumentException ae)
        {
            return Conflict(ae.Message);
        }
    }

    [HttpGet]
    public Task<ActionResult> Get(int id)
    {
        throw new NotImplementedException();
    }
}