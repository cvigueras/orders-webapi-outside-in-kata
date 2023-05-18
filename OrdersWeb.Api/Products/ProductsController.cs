using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace OrdersWeb.Api.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        return await Handle();
    }

    private async Task<IEnumerable<ProductReadDto>> Handle()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }
}