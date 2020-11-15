using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Category;
using ObmultichoiceRetailer.Web.Commands.Category;

namespace ObmultichoiceRetailer.Web.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryApiModel>()
                .ForMember(dest => dest.CategoryName,
                    mapOpt =>
                        mapOpt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription,
                    maptOpt =>
                        maptOpt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId,
                    maptOpt =>
                        maptOpt.MapFrom(src => src.Id));

            CreateMap<CreateCategoryApiModel, Category>()
                .ForMember(dest => dest.Name,
                    conf =>
                        conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description,
                    conf =>
                        conf.MapFrom(src => src.Description));

            CreateMap<CreateCategoryCommand, CreateCategoryApiModel>()
                .ReverseMap();
            CreateMap<UpdateCategoryCommand, UpdateCategoryApiModel>()
                .ForMember(d => d.CategoryId,
                    conf => 
                        conf.MapFrom(s => s.Id))
                .ForMember(d => d.CategoryName,
                    conf => 
                        conf.MapFrom(s => s.Name))
                .ForMember(d => d.CategoryDescription,
                    conf => 
                        conf.MapFrom(s => s.Description))
                .ReverseMap();
            CreateMap<UpdateCategoryApiModel, Category>()
                .ForMember(dest => dest.Name,
                    conf =>
                        conf.MapFrom(src => src.CategoryName))
                .ForMember(d => d.Id,
                    conf =>
                        conf.MapFrom(s => s.CategoryId))
                .ForMember(d => d.Description,
                    conf =>
                        conf.MapFrom(s => s.CategoryDescription))
                .ReverseMap();
        }
    }
}
