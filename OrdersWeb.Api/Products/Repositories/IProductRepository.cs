using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Products.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<int> Add(Product product);
    Task<Product> GetById(int id);
    Task<IEnumerable<Product>> GetProductsOrder(string orderNumber);
    Task<Product> GetByName(string? name);
}