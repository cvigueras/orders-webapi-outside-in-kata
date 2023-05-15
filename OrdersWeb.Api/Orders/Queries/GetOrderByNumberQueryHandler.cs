using AutoMapper;
using MediatR;

namespace OrdersWeb.Api.Orders.Queries;

public class GetOrderByNumberQueryHandler : IRequestHandler<GetOrderByNumberQuery, Order>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByNumberQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByOrderNumber(request.OrderNumber);
    }
}