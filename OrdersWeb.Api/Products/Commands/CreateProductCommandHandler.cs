using MediatR;
using OrdersWeb.Api.Products.Repositories;

namespace OrdersWeb.Api.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}