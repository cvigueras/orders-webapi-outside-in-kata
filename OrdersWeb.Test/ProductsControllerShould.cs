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

        [Test]
        public async Task RetrieveAllProductsList()
        {
            var productList = new List<Product>
            {
                new()
                {
                    Name = "Computer Monitor",
                    Price = "100€",
                },
                new()
                {
                    Name = "Keyboard",
                    Price = "30€",
                },
                new()
                {
                    Name = "Mouse",
                    Price = "15€",
                },
                new()
                {
                    Name = "Router",
                    Price = "70€",
                },
            };

            _productRepository.GetAll().Returns(productList);
            var productReadDto1 = new ProductReadDto("Computer Monitor", "100€");
            var productReadDto2 = new ProductReadDto("Keyboard", "30€");
            var productReadDto3 = new ProductReadDto("Mouse", "15€");
            var productReadDto4 = new ProductReadDto("Router", "70€");
            var expectedProducts = new List<ProductReadDto>
            {
                productReadDto1,
                productReadDto2,
                productReadDto3,
                productReadDto4
            };
            _mapper.Map<IEnumerable<ProductReadDto>>(productList).Returns(expectedProducts);

            var result = await _productsController.Get();

            result.Should().BeEquivalentTo(expectedProducts);

        }
    }
}
