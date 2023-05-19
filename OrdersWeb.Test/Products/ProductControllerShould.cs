using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrdersWeb.Api.Products.Controllers;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;
using OrdersWeb.Test.Products.Fixtures;

namespace OrdersWeb.Test.Products
{
    public class ProductControllerShould
    {
        private IMapper _mapper;
        private ISender _sender;
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _mapper = Substitute.For<IMapper>();
            _sender = Substitute.For<ISender>();
            _productRepository = Substitute.For<IProductRepository>();
        }

        [Test]
        public void GetEmptyProductWhenNotExist()
        {
            var productController = new ProductsController(_sender, _productRepository, _mapper);

            _productRepository.GetById(123).Returns((Product)null);
            var actionResult = productController.GetProductById(123);

            var result = actionResult.Result as NotFoundResult;

            result.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task RetrieveAnExistingProduct()
        {
            var productController = new ProductsController(_sender, _productRepository, _mapper);
            var expectedProduct = ProductMother.ComputerMonitorAsProductReadDto();
            var product = ProductMother.ComputerMonitorAsProduct();
            _productRepository.GetById(expectedProduct.Id).Returns(product);

            var actionResult = await productController.GetProductById(expectedProduct.Id);

            var result = actionResult as OkObjectResult;

            result.Value.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
