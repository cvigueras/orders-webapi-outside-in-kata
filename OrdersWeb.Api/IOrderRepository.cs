using OrdersWeb.Api.Models;

namespace OrdersWeb.Api;

public interface IOrderRepository
{
    void Add(Order order);
}