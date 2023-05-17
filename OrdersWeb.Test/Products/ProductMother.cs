using OrdersWeb.Api.Products;

namespace OrdersWeb.Test.Products;

public class ProductMother
{
    public static Product ComputerMonitorAsProduct()
    {
        return new Product
        {
            Name = "Computer Monitor",
            Price = "100€",
        };
    }
}