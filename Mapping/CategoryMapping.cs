using AutoMapper;
using eCommerce.Core.DTO.CategoryDTO;
using eCommerce.Core.Entities.Product;

namespace eCommerce.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddCategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            
        }
    }
  
}
