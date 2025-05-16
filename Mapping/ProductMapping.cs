using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities.Product;

namespace eCommerce.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping() // Changed from protected to public to ensure proper instantiation
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

           
            CreateMap<Photo,PhotoDTO>().ReverseMap();

            CreateMap< AddProductDTO, Product>()
                .ForMember(dest =>dest.Photos , opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap< UpdateProductDTO, Product>()
                .ForMember(dest =>dest.Photos , opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
