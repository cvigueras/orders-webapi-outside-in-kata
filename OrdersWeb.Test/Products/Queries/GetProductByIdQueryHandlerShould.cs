using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Queries;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Products.Fixtures;

namespace OrdersWeb.Test.Products.Queries
{
    public class GetProductByIdQueryHandlerShould
    {
        private IProductRepository productRepository;
        private GetProductByIdQueryHandler handler;
        private GetProductByIdQuery query;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            productRepository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
            query = new GetProductByIdQuery(1);
            handler = new GetProductByIdQueryHandler(productRepository, mapper);
        }


        [Test]
        public async Task RetrieveAnExistingProduct()
        {
            var expectedProduct = ProductMother.ComputerMonitorAsProductReadDto();
            var product = ProductMother.ComputerMonitorAsProduct();
            productRepository.GetById(1).Returns(product);
            mapper.Map<ProductReadDto>(product).Returns(expectedProduct);

            var result = await handler.Handle(query, default);

            result.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
