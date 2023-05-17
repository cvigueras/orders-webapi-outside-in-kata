using AutoMapper;
using MediatR;
using OrdersWeb.Api.Products;

namespace OrdersWeb.Api.Orders.Queries;

public class GetOrderByNumberQueryHandler : IRequestHandler<GetOrderByNumberQuery, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public GetOrderByNumberQueryHandler(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Order> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByOrderNumber(request.OrderNumber);
        var products = await _productRepository.GetProductsOrder(request.OrderNumber);
        orders.Products = products.ToList();
        return orders;
    }
}