using System.Data.SQLite;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api;

public class ProductRepository
{
    private readonly SQLiteConnection _connection;

    public ProductRepository(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public object GetAll()
    {
        return Enumerable.Empty<Product>();
    }
}