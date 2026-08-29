using AutoMapper;
using IceCreamEShop.Core.DTOs.Product;
using IceCreamEShop.Core.Entities;
using IceCreamEShop.Core.Enums;

namespace IceCreamEShop.Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.Category,
            tool => tool.MapFrom(ent => ((Category)ent.Category).ToString()));

            CreateMap<ProductDto, Product>()
                .ForMember(ent => ent.Category,
                           tool => tool.MapFrom(dto => Enum.Parse<Category>(dto.Category)));


            CreateMap<UpdateProductDto, Product>()
                .ForMember(ent => ent.Category,
                           tool => tool.MapFrom(dto => Enum.Parse<Category>(dto.Category)))
            // 如果來源欄位 (DTO) 是 null就忽略，不覆蓋到目的端 (Entity)
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
