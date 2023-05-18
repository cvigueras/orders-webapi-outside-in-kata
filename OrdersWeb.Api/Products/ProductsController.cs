using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace OrdersWeb.Api.Products;

public class GetAllProductsListQueryHandler
{
    private ProductsController _productsController;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsListQueryHandler(ProductsController productsController, IProductRepository productRepository,
        IMapper mapper)
    {
        _productsController = productsController;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductReadDto>> Handle()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }
}

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetAllProductsListQueryHandler _getAllProductsListQueryHandler;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _getAllProductsListQueryHandler = new GetAllProductsListQueryHandler(this, productRepository, _mapper);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        return await _getAllProductsListQueryHandler.Handle();
    }
}