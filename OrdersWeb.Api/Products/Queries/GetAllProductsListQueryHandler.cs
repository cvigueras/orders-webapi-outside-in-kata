using AutoMapper;
using MediatR;

namespace OrdersWeb.Api.Products.Queries;

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