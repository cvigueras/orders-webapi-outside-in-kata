using OrdersWeb.Api.Models;

namespace OrdersWeb.Api;

public interface IOrderRepository
{
    Task<int> Add(Order order);
    Task<Order> GetByOrderNumber(string number);
    Task Update(Order order);
}