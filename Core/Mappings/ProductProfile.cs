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
            CreateMap<AddUpdateProductDto, Product>().ReverseMap();
            CreateMap<ProductDto,Product>().ReverseMap();




        }
    }
}
