using OrdersWeb.Api.Models;
using AutoMapper;

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
