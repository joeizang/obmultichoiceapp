using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Category;

namespace RektaRetailApp.Web.Profiles
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
        }
    }
}
