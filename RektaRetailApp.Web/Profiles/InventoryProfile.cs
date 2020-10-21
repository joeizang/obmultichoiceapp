using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Inventory;
using RektaRetailApp.Web.Commands.Inventory;

namespace RektaRetailApp.Web.Profiles
{
  public class InventoryProfile : Profile
  {
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryApiModel>()
            .ForMember(d => d.CategoryName,
                conf =>
                    conf.MapFrom(s => s.Category.Name));

      CreateMap<CreateInventoryCommand, Inventory>();
    }
  }
}
