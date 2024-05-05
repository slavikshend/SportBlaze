using AutoMapper;
using webapi.BLL.Models;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.Support;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BrandModel, Brand>().ReverseMap();
        CreateMap<CategoryModel, Category>().ReverseMap();
        CreateMap<SubCategoryModel, SubCategory>().ReverseMap()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CategoryImageUrl, opt => opt.MapFrom(src => src.Category.Image));

        CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.BrandImageUrl, opt => opt.MapFrom(src => src.Brand.Image))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features))
            .ForMember(dest => dest.SubCategoryImageUrl, opt => opt.MapFrom(src => src.SubCategory.ImageUrl));

        CreateMap<ProductModel, Product>()
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId));

        CreateMap<FeatureModel, Feature>().ReverseMap();
    }
}
