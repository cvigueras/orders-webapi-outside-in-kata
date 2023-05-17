using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersWeb.Api;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Products;
using System.Data.SQLite;

namespace OrdersWeb.Test.Start;

public class StartupTest : WebApplicationFactory<Program>
{
    private readonly SQLiteConnection? _connection;

    public StartupTest()
    {
        _connection = new SQLiteConnection("Data Source=:memory:");

        _connection.Open();

        CreateDataBase();
    }

    public async Task CreateSeed()
    {
        if (!ExistTableProducts(_connection))
        {
            var productList = new List<Product>
            {
                new()
                {
                    Name = "Computer Monitor",
                    Price = "100€",
                },
                new()
                {
                    Name = "Keyboard",
                    Price = "30€",
                },
                new()
                {
                    Name = "Mouse",
                    Price = "15€",
                },
                new()
                {
                    Name = "Router",
                    Price = "70€",
                },
            };

            await _connection.ExecuteAsync($"INSERT INTO Products(Name, Price) " +
                                           $"VALUES(@Name, @Price)", productList);
        }
    }

    private void CreateDataBase()
    {
        _connection.Execute(@"CREATE TABLE IF NOT EXISTS Orders(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer VARCHAR(200) NOT NULL,
                Address VARCHAR(400) NOT NULL,
                Number VARCHAR(10) NOT NULL)"
        );

        _connection.Execute(@"CREATE TABLE IF NOT EXISTS Products(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name VARCHAR(200) NOT NULL,
                Price VARCHAR(20) NOT NULL)"
        );

        _connection.Execute(@"CREATE TABLE IF NOT EXISTS OrdersProducts(
                OrderNumber VARCHAR(10) NOT NULL,
                ProductId INTEGER NOT NULL)"
        );
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(_connection);
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        });

        return base.CreateHost(builder);
    }

    public SQLiteConnection? GetConnection()
    {
        return _connection;
    }

    private static bool ExistTableProducts(SQLiteConnection connection)
    {
        var exist = connection.Query<dynamic>("SELECT COUNT(*) as Count FROM Products");
        return exist.Single().Count > 1;
    }
}