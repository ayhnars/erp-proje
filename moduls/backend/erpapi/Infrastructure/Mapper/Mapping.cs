using AutoMapper;
using Entities;
using Entities.Dtos;

namespace erpapi.Infrastructure.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ErpUser, ErpUserDtoForUpdate>();
            CreateMap<ErpUser, ErpUserDtoForUpdate>().ReverseMap();
            CreateMap<ErpUserDtoforRegister, ErpUser>();
        }
    }
}