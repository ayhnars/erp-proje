using AutoMapper;
using Entities;
using Entities.Dtos;
using Entities.Dtos.UserDtos;

namespace erpapi.Infrastructure.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // ErpUser mapping'leri
            CreateMap<ErpUser, ErpUserDtoForUpdate>();
            CreateMap<ErpUser, ErpUserDtoForUpdate>().ReverseMap();
            CreateMap<ErpUserDtoforRegister, ErpUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}