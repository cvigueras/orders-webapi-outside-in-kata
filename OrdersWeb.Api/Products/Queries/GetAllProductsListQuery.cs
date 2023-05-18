using MediatR;
using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Products.Queries;

public class GetAllProductsListQuery : IRequest<IEnumerable<ProductReadDto>> { }