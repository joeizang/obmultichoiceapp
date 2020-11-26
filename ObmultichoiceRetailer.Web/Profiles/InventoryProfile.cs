using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;
using ObmultichoiceRetailer.Web.Commands.Inventory;

namespace ObmultichoiceRetailer.Web.Profiles
{
  public class InventoryProfile : Profile
  {
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryApiModel>()
            .ForMember(d => d.NumberOfProductsInStock,
                conf =>
                    conf.MapFrom(s => (float)s.InventoryItems.Count));

      CreateMap<CreateInventoryCommand, Inventory>();
    }
  }
}
