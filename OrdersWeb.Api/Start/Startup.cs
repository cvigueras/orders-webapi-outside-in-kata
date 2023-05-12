using System.Data.SQLite;

namespace OrdersWeb.Api.Start;

public class Startup
{
    public IConfiguration ConfigRoot
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IOrderRepository, OrderRepository>();
        Connection.CreateDataBase();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddScoped(_ => new SQLiteConnection("Data Source=./Orders.db"));
        services.AddAutoMapper(typeof(MapperConfig));
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}