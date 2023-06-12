using AutoMapper;
using MediatR;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;

namespace OrdersWeb.Api.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductReadDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductReadDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);
        return _mapper.Map<ProductReadDto>(product);
    }
}