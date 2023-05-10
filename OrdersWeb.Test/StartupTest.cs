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
        
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(_connection);
        });

        return base.CreateHost(builder);
    }

    public SQLiteConnection? GetConnection()
    {
        return _connection;
    }
}