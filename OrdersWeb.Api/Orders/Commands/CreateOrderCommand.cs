using MediatR;
using OrdersWeb.Api.Orders;

namespace OrdersWeb.Api.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public readonly OrderCreateDto OrderCreateDto;

    public CreateOrderCommand(OrderCreateDto orderCreateDto)
    {
        OrderCreateDto = orderCreateDto;
    }
}