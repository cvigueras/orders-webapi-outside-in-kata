using FluentAssertions;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Products.Fixtures;
using OrdersWeb.Test.Startup;
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
            var startupTest = new SetupFixture();
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
            var givenProduct = ProductMother.ComputerMonitorAsProduct();
            var id = await _repository.Add(givenProduct);

            var result = await _repository.GetById(id);

            var expectedProduct = ProductMother.ComputerMonitorAsProduct();
            expectedProduct.Id = id;
            result.Should().BeEquivalentTo(expectedProduct);
        }
    }
}