using AutoMapper;
using webapi.BLL.Models;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SubCategory, SubCategoryModel1>().ReverseMap();
        CreateMap<Category, CategoryModel1>().ReverseMap();


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
        CreateMap<FeedBack, FeedBackModel>().ReverseMap();

        CreateMap<Product, SimplifiedProductModel>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
    .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
    .ForMember(dest => dest.Rating, opt => opt.Ignore())
    .ForMember(dest => dest.IsFavourite, opt => opt.Ignore());

        CreateMap<Product, ProductDetailsModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory.Name))
            .ForMember(dest => dest.Feedbacks, opt =>opt.MapFrom(src => src.FeedBacks))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features));
    }
}
