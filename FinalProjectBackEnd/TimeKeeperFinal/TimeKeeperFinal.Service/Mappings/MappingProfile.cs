using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AccountDTO;
using TimeKeeperFinal.Service.DTOs.AddressInformationDTO;
using TimeKeeperFinal.Service.DTOs.BlogDTO;
using TimeKeeperFinal.Service.DTOs.BrandDTO;
using TimeKeeperFinal.Service.DTOs.CategoryDTO;
using TimeKeeperFinal.Service.DTOs.ColorDTO;
using TimeKeeperFinal.Service.DTOs.ModelDTO;
using TimeKeeperFinal.Service.DTOs.OrderDTO;
using TimeKeeperFinal.Service.DTOs.ProductDTO;
using TimeKeeperFinal.Service.DTOs.ProductItemDTO;
using TimeKeeperFinal.Service.DTOs.SizeDTO;
using TimeKeeperFinal.Service.DTOs.SliderDTO;
using TimeKeeperFinal.Service.DTOs.TagDTO;
using TimeKeeperFinal.Service.DTOs.UserDTO;

namespace TimeKeeperFinal.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Brand
            CreateMap<BrandPostDTO, Brand>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Brand, BrandListDTO>();
            CreateMap<Brand, BrandGetDTO>();
            #endregion
            #region Model
            CreateMap<ModelPostDTO, Model>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Model, ModelListDTO>();
            CreateMap<Model, ModelGetDTO>();
            #endregion
            #region Category
            CreateMap<CategoryPostDTO, Category>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Category, CategoryListDTO>();
            CreateMap<Category, CategoryGetDTO>();
            #endregion
            #region Product
            CreateMap<ProductPostDTO, Product>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Product,ProductListDTO>();
            CreateMap<Product,ProductGetDTO>();
            #endregion
            #region Color
            CreateMap<ColorPostDTO, Color>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Color, ColorGetDTO>();
            CreateMap<Color, ColorListDTO>();
            #endregion
            #region ProductItem
            CreateMap<ProductItemPostDTO,ProductItem>()
                .ForMember(des=>des.CreatedAt,src=>src.MapFrom(s=>DateTime.UtcNow.AddHours(4)));
            CreateMap<ProductItem, ProductItemListDTO>();
            CreateMap<ProductItem, ProductItemGetDTO>();
            #endregion
            #region Tag
            CreateMap<TagPostDTO, Tag>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Tag, TagGetDTO>();
            CreateMap<Tag, TagListDTO>();
            #endregion
            #region Account
            CreateMap<RegisterDTO, AppUser>();
            #endregion
            #region Slider
            CreateMap<SliderPostDTO, Slider>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Slider, SliderGetDTO>();
            CreateMap<Slider, SliderListDTO>();
            #endregion
            #region Blog
            CreateMap<BlogPostDTO, Blog>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Blog, BlogGetDTO>();
            CreateMap<Blog, BlogListDTO>();
            #endregion
            #region Size
            CreateMap<SizePostDTO, Size>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Size, SizeGetDTO>();
            CreateMap<Size, SizeListDTO>();
            #endregion
            #region User
            CreateMap<UserRegisterDTO, AppUser>();
            CreateMap<AppUser, UserListDTO>();
            #endregion
            #region AddressInformation
            CreateMap<AddressInformationPostDTO, AddressInformation>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<AddressInformation, AddressInformationGetDTO>();
            CreateMap<AddressInformation, AddressInformationListDTO>();
            #endregion
            #region Order
            CreateMap<OrderPostDTO, Order>()
               .ForMember(des => des.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow.AddHours(4)));
            CreateMap<Order, OrderGetDTO>();
            CreateMap<Order, OrderListDTO>();
            #endregion
        }
    }
}
