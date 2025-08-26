using AutoMapper;
using Entities;
using Entities.Dtos;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.CompanyDtos;
using Entities.Dtos.CustomerDtos;
using Entities.Dtos.ProductDtos;
using Entities.Dtos.StockMovementDtos;
using Entities.Dtos.UserDtos;
using Entities.Models;

namespace erpapi.Infrastructure.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // ErpUser mapping
            CreateMap<ErpUser, ErpUserDtoForUpdate>().ReverseMap();
            CreateMap<ErpUserDtoforRegister, ErpUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            // Company mapping
            CreateMap<CompanyDtoForCreate, Company>();
            CreateMap<CompanyDtoForUpdate, Company>().ReverseMap();

            // Category mapping
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDtoForInsert, Category>();
            CreateMap<CategoryDtoForUpdate, Category>();

            // Product mapping
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDtoForInsert, Product>();
            CreateMap<ProductDtoForUpdate, Product>();

            // Customer mapping
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDtoForInsert, Customer>();
            CreateMap<CustomerDtoForUpdate, Customer>();

            // StokMovement mapping
            CreateMap<StockMovement, StockMovementDto>().ReverseMap();
            CreateMap<StockMovementDtoForInsert, StockMovement>();
            CreateMap<StockMovementDtoForUpdate, StockMovement>();
        }
    }
}
