using OrdersWeb.Api.Orders.Models;

namespace OrdersWeb.Api.Orders.Repositories;

public interface IOrderRepository
{
    Task<int> Add(Order order);
    Task<Order> GetByOrderNumber(string number);
    Task Update(Order order);
}