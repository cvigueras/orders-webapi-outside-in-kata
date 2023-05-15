using MediatR;
using OrdersWeb.Api.Orders;

namespace OrdersWeb.Api.Orders.Commands;

public class UpdateOrderCommand : IRequest
{
    public readonly OrderUpdateDto OrderUpdateDto;

    public UpdateOrderCommand(OrderUpdateDto orderUpdateDto)
    {
        OrderUpdateDto = orderUpdateDto;
    }
}