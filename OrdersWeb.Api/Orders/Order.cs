namespace OrdersWeb.Api.Orders;

public class Order
{
    protected bool Equals(Order other)
    {
        return Number == other.Number && Customer == other.Customer && Address == other.Address;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Order)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Number, Customer, Address);
    }

    public int Id { get; set; }
    public string? Number { get; set; }
    public string? Customer { get; set; }
    public string? Address { get; set; }
}