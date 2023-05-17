using OrdersWeb.Api.Orders;

namespace OrdersWeb.Test.Orders.Fixtures;

public class OrderMother
{
    public static Order JohnDoeAsCustomer()
    {
        return new Order
        {
            Number = "ORD445190",
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
}