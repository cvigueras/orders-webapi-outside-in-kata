using OrdersWeb.Api.Products;

namespace OrdersWeb.Test.Products.Fixtures;

public class ProductMother
{

    public static Product KeyboardAsProduct()
    {
        return new()
        {
            Id = 2,
            Name = "Keyboard",
            Price = "30€",
        };
    }

    public static Product ComputerMonitorAsProduct()
    {
        return new()
        {
            Id = 1,
            Name = "Computer Monitor",
            Price = "100€",
        };
    }

    public static Product MouseAsProduct()
    {
        return new()
        {
            Id = 1,
            Name = "Mouse",
            Price = "100€",
        };
    }

    public static Product RouterAsProduct()
    {
        return new()
        {
            Id = 1,
            Name = "Router",
            Price = "70€",
        };
    }

    public static ProductReadDto KeyboardAsProductReadDto()
    {
        return new ProductReadDto(0, "Keyboard", "30€");
    }

    public static ProductReadDto ComputerMonitorAsProductReadDto()
    {
        return new ProductReadDto(0, "Computer Monitor", "100€");
    }

    public static ProductReadDto MouseAsProductReadDto()
    {
        return new ProductReadDto(0, "Mouse", "15€");
    }

    public static ProductReadDto RouterAsProductReadDto()
    {
        return new ProductReadDto(0, "Router", "70€");
    }
}