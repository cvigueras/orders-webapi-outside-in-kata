using FluentAssertions;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class ProductsControllerShould
    {
        private ProductsController _productsController;

        [SetUp]
        public void SetUp()
        {
            _productsController = new ProductsController();
        }

        [Test]
        public void GetEmptyProductsList()
        {
            var result = _productsController.Get();

            result.Should().Be(Enumerable.Empty<ProductReadDto>());
        }
    }
}
