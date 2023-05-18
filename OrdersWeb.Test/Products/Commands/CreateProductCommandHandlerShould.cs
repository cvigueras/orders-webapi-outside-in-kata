using FluentAssertions;
using OrdersWeb.Api.Products.Commands;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Startup;
using System.Data.SQLite;
using AutoMapper;
using NSubstitute;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Test.Products.Fixtures;
using NSubstitute.ExceptionExtensions;

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
        public void FailWhenSendToPostAnExistingProduct()
        {
            var productCreateDto = new ProductCreateDto("Mouse", "15€");
            var createProductCommand = new CreateProductCommand(productCreateDto);
            var product = ProductMother.MouseAsProduct();
            _mapper.Map<Product>(productCreateDto).Returns(product);

            var action = () => createProductCommandHandler.Handle(createProductCommand, default);

            action.Should().ThrowAsync<ArgumentException>().WithMessage("Product already exist");
        }
    }
}
