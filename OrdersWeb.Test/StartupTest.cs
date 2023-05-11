using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersWeb.Api;
using System.Data.SQLite;

namespace OrdersWeb.Test;

public class StartupTest : WebApplicationFactory<Program>
{
    private readonly SQLiteConnection? _connection;

    public StartupTest()
    {
        _connection = new SQLiteConnection("Data Source=:memory:");

        _connection.Open();

        CreateDataBase();
    }

    private void CreateDataBase()
    {
        _connection.Execute(@"CREATE TABLE IF NOT EXISTS Orders(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer VARCHAR(200) NOT NULL,
                Address VARCHAR(400) NOT NULL,
                Number VARCHAR(10) NOT NULL)"
        );
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(_connection);
            services.AddSingleton<IOrderRepository,OrderRepository>();
        });

        return base.CreateHost(builder);
    }

    public SQLiteConnection? GetConnection()
    {
        return _connection;
    }
}