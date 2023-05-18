using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Orders.Models;

public record OrderReadDto(string Number, string Customer, string Address, List<Product> Products);