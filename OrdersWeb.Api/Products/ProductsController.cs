using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OrdersWeb.Api.Products;

public class GetAllProductsListQueryHandler : IRequestHandler<GetAllProductsListQuery, IEnumerable<ProductReadDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsListQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductReadDto>> Handle(GetAllProductsListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }
}

public class GetAllProductsListQuery : IRequest<IEnumerable<ProductReadDto>> { }

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