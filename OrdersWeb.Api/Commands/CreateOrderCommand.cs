using MediatR;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public readonly OrderCreateDto OrderCreateDto;

    public CreateOrderCommand(OrderCreateDto orderCreateDto)
    {
        OrderCreateDto = orderCreateDto;
    }
}