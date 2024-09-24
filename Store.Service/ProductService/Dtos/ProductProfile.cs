using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.ProductService.Dtos
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest =>dest.BrandName,options=>options.MapFrom(src=>src.Brand.Name))
                .ForMember(dest =>dest.TypeName,options=>options.MapFrom(src=>src.Type.Name));
            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();

        }
    }
}
