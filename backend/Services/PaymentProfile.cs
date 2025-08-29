using AutoMapper;
using Entities.Dtos.PaymentDtos;
using Entities.Models;

namespace Services.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(d => d.PaymentMethod, m => m.MapFrom(s => s.PaymentMethod.ToString()))
                .ForMember(d => d.PaymentStatus, m => m.MapFrom(s => s.PaymentStatus.ToString()));

            CreateMap<PaymentForCreateDto, Payment>()
                .ForMember(d => d.PaymentMethod, m => m.MapFrom(s => Enum.Parse<PaymentMethod>(s.PaymentMethod, true)))
                .ForMember(d => d.PaymentStatus, m => m.MapFrom(s => Enum.Parse<PaymentStatus>(s.PaymentStatus, true)))
                .ForMember(d => d.PaymentDate, m => m.MapFrom(s => s.PaymentDate ?? DateTime.UtcNow));

            CreateMap<PaymentForUpdateDto, Payment>()
                .ForMember(d => d.PaymentStatus, m => m.MapFrom(s => Enum.Parse<PaymentStatus>(s.PaymentStatus, true)));
        }
    }
}
