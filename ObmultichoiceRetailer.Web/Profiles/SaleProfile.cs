using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Commands.Sales;

namespace ObmultichoiceRetailer.Web.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ConstructUsing(s => new Sale()
                {
                    SaleDate = s.SaleDate,
                    SalesPerson = s.SalesPerson!,
                    ModeOfPayment = s.PaymentType,
                    GrandTotal = s.GrandTotal,
                    SubTotal = s.SubTotal,
                    TypeOfSale = s.SaleType
                });
            CreateMap<Sale, SaleApiModel>()
                .ConstructUsing(s => new SaleApiModel
                {
                    Id = s.Id,
                    SalesPerson = s.SalesPerson,
                    GrandTotal = s.GrandTotal,
                    SaleDate = s.SaleDate,
                    NumberOfItemsSold = s.ItemsSold.Count,
                });
        }
    }
}
