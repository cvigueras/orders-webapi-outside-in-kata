using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Orders.Models;

public record OrderCreateDto(string Number, string Customer, string Address, List<Product> Products);