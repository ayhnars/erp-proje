using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // Order → OrderDetailsDto: Items'ı servis dolduracak
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(d => d.Items, opt => opt.Ignore());
        }
    }
}
