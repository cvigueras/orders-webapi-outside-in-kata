using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Products.Commands;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Products.Fixtures;
using OrdersWeb.Test.Startup;
using System.Data.SQLite;

namespace OrdersWeb.Test.Products.Commands
{
    public class CreateProductCommandHandlerShould
    {
        private SetupFixture _client;
        private SQLiteConnection? _connection;
        private IProductRepository _productRepository;
        private CreateProductCommandHandler createProductCommandHandler;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _client = new SetupFixture();
            _connection = _client.GetConnection();
            _client.CreateSeed();
            _productRepository = new ProductRepository(_connection);
            _mapper = Substitute.For<IMapper>();
            createProductCommandHandler = new CreateProductCommandHandler(_productRepository, _mapper);
        }

        [Test]
        public async Task FailWhenSendToPostAnExistingProduct()
        {
            var product = ProductMother.MouseAsProduct();
            var productCreateDto = new ProductCreateDto("Mouse", "15€");
            _mapper.Map<Product>(Arg.Is(productCreateDto)).Returns(product);

            var createProductCommand = new CreateProductCommand(productCreateDto);
            var action = () => createProductCommandHandler.Handle(createProductCommand, default);

            await action.Should().ThrowAsync<ArgumentException>().WithMessage("Product already exist");
        }

        [Test]
        public async Task RetrieveANewProductAfterPost()
        {
            var product = new Product
            {
                Id = 5,
                Name = "Headphones",
                Price = "90€",
            };
            var productCreateDto = new ProductCreateDto("Headphones", "90€");
            _mapper.Map<Product>(productCreateDto).Returns(product);
            var createProductCommand = new CreateProductCommand(productCreateDto);
            var id = await createProductCommandHandler.Handle(createProductCommand, default);

            var expectedProduct = new ProductReadDto(id,"Headphones", "90€");

            var result = await _productRepository.GetById(id);
            result.Should().BeEquivalentTo(expectedProduct);
        }
    }
}