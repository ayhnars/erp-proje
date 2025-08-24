using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // OrderItem ↔ OrderItemDto
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // Order → OrderDetailsDto (Items servis katmanında doldurulacak)
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(d => d.Items, opt => opt.Ignore());

            // ModuleCart → ModuleCartDto (Status enum'u string'e çevrilir)
            CreateMap<ModuleCart, ModuleCartDto>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
        }
    }
}

