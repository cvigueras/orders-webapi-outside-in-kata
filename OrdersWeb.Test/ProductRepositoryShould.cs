using FluentAssertions;
using OrdersWeb.Api;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class ProductRepositoryShould
    {
        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new ProductRepository();
        }

        [Test]
        public void HaveNoProductsInitially()
        {
            var result = _repository.GetAll();

            result.Should().Be(Enumerable.Empty<Product>());
        }
    }
}
