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

    public async Task<int> Add(Order order)
    {
        await _connection.ExecuteAsync($"INSERT INTO Orders(Customer, Address, Number) " +
                                       $"VALUES('{order.Customer}', '{order.Address}', '{order.Number}');");
        var foo = _connection.ExecuteScalar<string>("select last_insert_rownumber()");
        return _connection.ExecuteScalar<int>("select last_insert_rowid()");
    }

    public Task<Order> GetByOrderNumber(string number)
    {
        var orders = _connection.Query<Order>($"SELECT * FROM ORDERS WHERE Number = '{number}'");
        return Task.FromResult(orders.First());
    }

    public async Task Update(Order order)
    {
        await _connection.ExecuteAsync($"UPDATE Orders SET Customer = '{order.Customer}', Address = '{order.Address}'" +
                                       $" WHERE Number = '{order.Number}'");
    }
}