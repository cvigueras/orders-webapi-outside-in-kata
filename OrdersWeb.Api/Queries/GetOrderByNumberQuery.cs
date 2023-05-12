using MediatR;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Queries;

public class GetOrderByNumberQuery : IRequest<Order>
{
    public GetOrderByNumberQuery(string orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public string OrderNumber { get; set; }
}