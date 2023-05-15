using Dapper;
using OrdersWeb.Api.Models;
using System.Data.SQLite;

namespace OrdersWeb.Api;

public class ProductRepository : IProductRepository
{
    private readonly SQLiteConnection _connection;

    public ProductRepository(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _connection.QueryAsync<Product>($"SELECT * FROM Products");
    }

    public async Task<int> Add(Product product)
    {
        await _connection.ExecuteAsync($"INSERT INTO Products(Name, Price) " +
                                       $"VALUES('{product.Name}', '{product.Price}');"); 
        return GetLastId();
    }

    public async Task<Product> GetById(int id)
    {
        var products = await _connection.QueryAsync<Product>($"SELECT * FROM Products WHERE Id = '{id}'");
        return products.First();
    }

    private int GetLastId()
    {
        return _connection.ExecuteScalar<int>("SELECT MAX(id) FROM Products");
    }
}