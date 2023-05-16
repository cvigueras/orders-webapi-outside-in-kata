using OrdersWeb.Api.Products;

namespace OrdersWeb.Api.Orders;

public record OrderCreateDto(string Number, string Customer, string Address, List<Product> Products);