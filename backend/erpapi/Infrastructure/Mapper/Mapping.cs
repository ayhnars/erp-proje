using AutoMapper;
using Entities;
using Entities.Dtos;
using Entities.Dtos.CompanyDtos;
using Entities.Dtos.UserDtos;
using Entities.Models;

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
            CreateMap<CompanyDtoForCreate, Company>();
            CreateMap<CompanyDtoForUpdate, Company>().ReverseMap();
            CreateMap<CompanyDtoForUpdate, Company>();
<<<<<<< HEAD
            CreateMap<Modules, ModuleDtoForCreate>().ReverseMap();
            CreateMap<Modules, ModuleDtoForUpdate>().ReverseMap();
=======
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)

        }
    }
}