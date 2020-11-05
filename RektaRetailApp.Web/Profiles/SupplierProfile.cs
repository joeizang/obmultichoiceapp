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
                .ConstructUsing(s =>
                    new Supplier(s.Name!.Trim().ToUpperInvariant(), s.PhoneNumber!.Trim().ToUpperInvariant(),
                        s.Description!.Trim().ToUpperInvariant()));

                    CreateMap<Supplier, SupplierApiModel>()
                .ConstructUsing(s => new SupplierApiModel(s.Name, s.MobileNumber, s.Id));
            CreateMap<SupplierApiModel, Supplier>()
                .ConstructUsing(s => new Supplier
                {
                    Id = s.SupplierId,
                    Name = s.Name ?? "",
                    MobileNumber = s.MobileNumber ?? ""
                });

            CreateMap<Supplier, SupplierDetailApiModel>()
                .ConstructUsing(s => new SupplierDetailApiModel(s.Name, s.MobileNumber, s.Description));

        }
    }
}
