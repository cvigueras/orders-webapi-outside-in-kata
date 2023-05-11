using OrdersWeb.Api.Models;

namespace OrdersWeb.Api;

public interface IOrderRepository
{
    Task Add(Order order);
}