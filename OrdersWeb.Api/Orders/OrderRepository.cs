using Dapper;
using System.Data.SQLite;

namespace OrdersWeb.Api.Orders;

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

        if (order.Products != null && order.Products.Any())
        {
            foreach (var product in order.Products)
            {
                await _connection.ExecuteAsync(
                    $"INSERT INTO OrdersProducts (OrderNumber, ProductId) VALUES ('{order.Number}', '{product.Id}');");
            }
        }

        return GetLastId();
    }

    public async Task<Order> GetByOrderNumber(string number)
    {
        return (await _connection.QueryAsync<Order>($"SELECT * FROM ORDERS WHERE Number = '{number}'")).First();
    }

    public async Task Update(Order order)
    {
        await _connection.ExecuteAsync($"UPDATE Orders SET Customer = '{order.Customer}', Address = '{order.Address}'" +
                                       $" WHERE Number = '{order.Number}'");
    }
    private int GetLastId()
    {
        return _connection.ExecuteScalar<int>("SELECT MAX(id) FROM Orders");
    }
}