using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Products;

namespace OrdersWeb.Test.Orders.Fixtures;

public class OrderMother
{
    public static Order JohnDoeAsCustomer()
    {
        return new Order
        {
            Number = "ORD765190",
            Customer = "John Doe",
            Address = "A Simple Street, 123",
        };
    }

    public static Order NewCustomerAsCustomer(int lastId, Order givenOrder)
    {
        return new Order
        {
            Id = lastId,
            Address = "New Address",
            Customer = "New customer",
            Number = givenOrder.Number,
        };
    }

    public static Order JohnDoeAsCustomerWithZeroProducts()
    {
        return new Order
        {
            Number = "ORD765190",
            Customer = "John Doe",
            Address = "A Simple Street, 123",
            Products = new List<Product>(),
        };
    }

    public static Order ACustomerAsCustomerWithTwoProducts()
    {
        return new Order
        {
            Customer = "A customer",
            Number = "ORD765190",
            Address = "An Address",
            Products = new List<Product>
            {
                new()
                {
                    Id = 1,
                    Name = "Computer Monitor",
                    Price = "100€",
                },
                new()
                {
                    Id = 2,
                    Name = "Keyboard",
                    Price = "30€",
                }
            }
        };
    }
}