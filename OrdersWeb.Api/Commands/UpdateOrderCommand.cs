using MediatR;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Commands;

public class UpdateOrderCommand : IRequest
{
    public readonly OrderUpdateDto OrderUpdateDto;

    public UpdateOrderCommand(OrderUpdateDto orderUpdateDto)
    {
        OrderUpdateDto = orderUpdateDto;
    }
}