using Dapper;
using OrdersWeb.Api.Models;
using System.Data.SQLite;

namespace OrdersWeb.Api;

public class OrderRepository : IOrderRepository
{
    private readonly SQLiteConnection? _connection;

    public OrderRepository(SQLiteConnection? connection)
    {
        _connection = connection;
    }

    public async Task Add(Order order)
    {
        await _connection.ExecuteAsync($"INSERT INTO Orders(Customer, Address, Number) " +
                                       $"VALUES('{order.Customer}', '{order.Address}', '{order.Number}')");
    }

    public Task<Order> GetByOrderNumber(string number)
    {
        var orders = _connection.Query<Order>($"SELECT * FROM ORDERS WHERE Number = '{number}'");
        return Task.FromResult(orders.First());
    }

    public void Update(Order expectedOrder)
    {
        throw new NotImplementedException();
    }
}