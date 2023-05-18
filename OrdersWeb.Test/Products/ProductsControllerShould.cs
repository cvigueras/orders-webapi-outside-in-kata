using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Products;
using OrdersWeb.Test.Products.Fixtures;

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
            var productList = GetFirstFourProducts();
            _productRepository.GetAll().Returns(productList);
            var expectedProducts = GivenFirstDtoProducts();
            _mapper.Map<IEnumerable<ProductReadDto>>(productList).Returns(expectedProducts);

            var result = await _productsController.Get();

            result.Should().BeEquivalentTo(expectedProducts);

        }

        private static List<ProductReadDto> GivenFirstDtoProducts()
        {
            return new List<ProductReadDto>
            {
                ProductMother.ComputerMonitorAsProductReadDto(),
                ProductMother.KeyboardAsProductReadDto(),
                ProductMother.MouseAsProductReadDto(),
                ProductMother.RouterAsProductReadDto(),
            };
        }

        private static List<Product> GetFirstFourProducts()
        {
            return new List<Product>
            {
                ProductMother.ComputerMonitorAsProduct(),
                ProductMother.KeyboardAsProduct(),
                ProductMother.MouseAsProduct(),
                ProductMother.RouterAsProduct(),
            };
        }
    }
}