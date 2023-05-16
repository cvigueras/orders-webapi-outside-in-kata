using FluentAssertions;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Start;
using System.Data.SQLite;

namespace OrdersWeb.Test.Products
{
    public class ProductRepositoryShould
    {
        private SQLiteConnection? connection;
        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            var startupTest = new StartupTest();
            connection = startupTest.GetConnection();
            _repository = new ProductRepository(connection);
        }

        [Test]
        public async Task HaveNoProductsInitially()
        {
            var result = await _repository.GetAll();

            result.Should().BeEquivalentTo(Enumerable.Empty<Product>());
        }

        [Test]
        public async Task RetrieveAnExistingProduct()
        {
            var givenProduct = new Product
            {
                Name = "Product1",
                Price = "100€",
            };
            var id = await _repository.Add(givenProduct);

            var result = await _repository.GetById(id);

            var expectedProduct = new Product
            {
                Id = id,
                Name = "Product1",
                Price = "100€",
            };
            result.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
