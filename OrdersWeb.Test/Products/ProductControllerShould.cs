using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OrdersWeb.Api.Products.Controllers;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Repositories;

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
            var actionResult = productController.Get(123);

            var result = actionResult.Result as ConflictObjectResult;

            result.Value.Should().Be("The product not exists");
            result.StatusCode.Should().Be(409);
        }
    }
}
