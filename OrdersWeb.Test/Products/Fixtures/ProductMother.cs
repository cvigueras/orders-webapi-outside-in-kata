using OrdersWeb.Api.Products;

namespace OrdersWeb.Test.Products.Fixtures;

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