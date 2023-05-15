using System.Data.SQLite;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api;
using OrdersWeb.Api.Controllers;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Test
{
    public class ProductsControllerShould
    {
        private ProductsController _productsController;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _productsController = new ProductsController(_productRepository, _mapper);
        }

        [Test]
        public async Task GetEmptyProductsList()
        {
            _productRepository.GetAll().Returns(Enumerable.Empty<Product>());
            
            var result = await _productsController.Get();

            result.Should().BeEquivalentTo(Enumerable.Empty<ProductReadDto>());
        }
    }
}
