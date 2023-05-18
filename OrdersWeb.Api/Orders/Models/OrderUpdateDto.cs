namespace OrdersWeb.Api.Orders.Models;

public record OrderUpdateDto(int Id, string Number, string Customer, string Address, List<int> ProductsId);