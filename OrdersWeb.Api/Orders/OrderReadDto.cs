using OrdersWeb.Api.Products;

namespace OrdersWeb.Api.Orders;

public record OrderReadDto(string Number, string Customer, string Address, List<Product> Products);