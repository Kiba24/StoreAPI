using AutoMapper;
using Core.Dtos.Brand;
using Core.Dtos.Category;
using Core.Dtos.Product;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<BrandDto, Brand>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Brand, opt => opt.Ignore())  // Ignore Brand navigation property
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ReverseMap();
                
  

            CreateMap<ProductDto, Product>()
                   .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                   .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                                   .ForMember(dest => dest.Brand, opt => opt.Ignore())  // Ignore Brand navigation property
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                   .ReverseMap();



        }
    }
}
