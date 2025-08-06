using AutoMapper;
using Entities;
using Entities.Dtos;

namespace erpapi.Infrastructure.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ErpUser, ErpUserForUpdate>();
            CreateMap<ErpUser, ErpUserForUpdate>().ReverseMap();
            CreateMap<ErpUserDtoforRegister, ErpUser>();
            CreateMap<ErpUserDtoForLogin, ErpUser>();
        }
    }
}