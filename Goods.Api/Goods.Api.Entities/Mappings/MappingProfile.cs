using AutoMapper;
using Goods.Api.Goods.Api.Entities.Concrete;
using Goods.Api.Goods.Api.Entities.Dtos;

namespace Goods.Api.Goods.Api.Entities.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryCreatedDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();



            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreatedDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

        }
    }
}