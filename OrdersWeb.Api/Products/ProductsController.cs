using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Queries;

namespace OrdersWeb.Api.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public ProductsController(IProductRepository productRepository, IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        var query = new GetAllProductsListQuery();
        return await _sender.Send(query);
    }
}