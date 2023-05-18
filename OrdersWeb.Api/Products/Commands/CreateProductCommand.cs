using MediatR;
using OrdersWeb.Api.Products.Models;

namespace OrdersWeb.Api.Products.Commands;

public class CreateProductCommand: IRequest<int>
{
    public CreateProductCommand(ProductCreateDto productCreateDto)
    {
        ProductCreate = productCreateDto;
    }

    public ProductCreateDto ProductCreate { get; set; }

}