using AutoMapper;
using OrdersWeb.Api.Models;

namespace OrdersWeb.Api.Start
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderReadDto>().ReverseMap();
        }
    }
}
