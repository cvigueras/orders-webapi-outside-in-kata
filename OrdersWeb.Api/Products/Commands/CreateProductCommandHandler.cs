using AutoMapper;
using MediatR;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;

namespace OrdersWeb.Api.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private IProductRepository _productRepository;
    private IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existProduct = await _productRepository.GetByName(request.ProductCreate.Name);
        if (existProduct != null)
        {
            throw new ArgumentException("Product already exist");
        }
        var product = _mapper.Map<Product>(request.ProductCreate);
        return await _productRepository.Add(product);
    }
}