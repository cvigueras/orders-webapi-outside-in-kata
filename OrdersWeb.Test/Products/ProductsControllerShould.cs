using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Products;

namespace OrdersWeb.Test.Products
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
            var productReadDto1 = new ProductReadDto(0, "Computer Monitor", "100€");
            var productReadDto2 = new ProductReadDto(0, "Keyboard", "30€");
            var productReadDto3 = new ProductReadDto(0, "Mouse", "15€");
            var productReadDto4 = new ProductReadDto(0, "Router", "70€");
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
