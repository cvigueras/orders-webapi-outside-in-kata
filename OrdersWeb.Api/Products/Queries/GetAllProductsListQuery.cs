using MediatR;

namespace OrdersWeb.Api.Products.Queries;

public class GetAllProductsListQuery : IRequest<IEnumerable<ProductReadDto>> { }