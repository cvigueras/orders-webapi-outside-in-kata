using MediatR;
using OrdersWeb.Api.Orders;

namespace OrdersWeb.Api.Orders.Queries;

public class GetOrderByNumberQuery : IRequest<Order>
{
    public GetOrderByNumberQuery(string orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public string OrderNumber { get; set; }
}