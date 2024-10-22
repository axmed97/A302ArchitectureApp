using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.BrandDTOs;

namespace Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        // Create - DTO - Model
        // Get - Model - DTO
        public MappingProfile()
        {
            CreateMap<Brand, AddBrandDTO>().ReverseMap();
            CreateMap<UpdateBrandDTO, Brand>();
        }
    }
}
