using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Queries;

namespace OrdersWeb.Api.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly GetAllProductsListQueryHandler _getAllProductsListQueryHandler;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _getAllProductsListQueryHandler = new GetAllProductsListQueryHandler(productRepository, _mapper);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        var query = new GetAllProductsListQuery();
        return await _getAllProductsListQueryHandler.Handle(query, default);
    }
}