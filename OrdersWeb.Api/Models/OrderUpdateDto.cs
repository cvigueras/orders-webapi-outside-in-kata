namespace OrdersWeb.Api.Models;

public record OrderUpdateDto(int Id, string Number, string Customer, string Address);