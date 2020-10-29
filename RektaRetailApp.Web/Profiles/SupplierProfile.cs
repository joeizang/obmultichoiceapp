using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;

namespace RektaRetailApp.Web.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<CreateSupplierCommand, Supplier>()
                .ForMember(d => d.Name, conf => conf.MapFrom(s => s.Name!.Trim().ToUpperInvariant()))
                .ForMember(d => d.MobileNumber, conf => conf.MapFrom(s => s.PhoneNumber!.Trim().ToUpperInvariant()))
                .ForMember(d => d.Description, conf => conf.MapFrom(s => s.Description!.Trim().ToUpperInvariant()))
                .ReverseMap();

            CreateMap<Supplier, SupplierApiModel>()
                .ForMember(d => d.SupplierId, conf => conf.MapFrom(s => s.Id));

            CreateMap<Supplier, SupplierDetailApiModel>()
                .ForMember(d => d.ProductIds, conf => conf.MapFrom(s => s.ProductsSupplied.Select(x => x.Id)));

        }
    }
}
