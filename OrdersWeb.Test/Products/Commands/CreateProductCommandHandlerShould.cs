using FluentAssertions;
using OrdersWeb.Api.Products.Commands;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Startup;
using System.Data.SQLite;
using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Test.Products.Commands
{
    public class CreateProductCommandHandlerShould
    {
        private SetupFixture _client;
        private SQLiteConnection? _connection;
        private IProductRepository _productRepository;
        private CreateProductCommandHandler createProductCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _client = new SetupFixture();
            _connection = _client.GetConnection();
            _productRepository = new ProductRepository(_connection);
            createProductCommandHandler = new CreateProductCommandHandler(_productRepository);
        }

        [Test]
        public void FailWhenSendToPostAnExistingProduct()
        {
            var productCreateDto = new ProductCreateDto("Headphones", "90€");
            var createProductCommand = new CreateProductCommand(productCreateDto);

            Action action = () => createProductCommandHandler.Handle(createProductCommand, default);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
