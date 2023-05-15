using AutoMapper;
using OrdersWeb.Api.Orders;
using OrdersWeb.Api.Products;

namespace OrdersWeb.Api.Start
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderReadDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
            CreateMap<Product, ProductReadDto>().ReverseMap();
        }
    }
}
