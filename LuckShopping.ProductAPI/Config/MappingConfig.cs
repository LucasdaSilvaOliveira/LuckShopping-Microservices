using AutoMapper;
using LuckShopping.ProductAPI.Data.Entites;
using LuckShopping.ProductAPI.DTOs;

namespace LuckShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<Product, ProductDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
