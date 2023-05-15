using FluentAssertions;
using OrdersWeb.Api;
using OrdersWeb.Api.Models;
using System.Data.SQLite;

namespace OrdersWeb.Test
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
        public void HaveNoProductsInitially()
        {
            var result = _repository.GetAll();

            result.Should().Be(Enumerable.Empty<Product>());
        }

        [Test]
        public void RetrieveAnExistingProduct()
        {
            var givenProduct = new Product
            {

                Name = "Product1",
                Price = "100€",
            };
            _repository.Add(givenProduct);

            var result = _repository.GetAll();

            var expectedProduct = new Product
            {
                Name = "Product1",
                Price = "100€",
            };
            result.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
