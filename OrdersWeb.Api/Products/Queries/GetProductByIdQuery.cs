using MediatR;
using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Products.Queries;

public class GetProductByIdQuery : IRequest<ProductReadDto>
{
    public GetProductByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}