namespace OrdersWeb.Api.Orders;

public record OrderUpdateDto(int Id, string Number, string Customer, string Address, List<int> ProductsId);