namespace OrdersWeb.Api.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<int> Add(Product product);
    Task<Product> GetById(int id);
}