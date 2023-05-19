﻿using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersWeb.Api.Products.Commands;
using OrdersWeb.Api.Products.Models;
using OrdersWeb.Api.Products.Queries;
using OrdersWeb.Api.Products.Repositories;

namespace OrdersWeb.Api.Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(ISender sender, IProductRepository productRepository, IMapper mapper)
    {
        _sender = sender;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductReadDto>> Get()
    {
        var query = new GetAllProductsListQuery();
        return await _sender.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
    {
        try
        {
            var query = new CreateProductCommand(productCreateDto);
            return Ok(await _sender.Send(query));
        }
        catch (ArgumentException ae)
        {
            return Conflict(ae.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}