using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Category;
using ObmultichoiceRetailer.Web.ApiModel.Product;
using ObmultichoiceRetailer.Web.Commands.Product;

namespace ObmultichoiceRetailer.Web.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductApiModel>();
            CreateMap<ProductApiModel, Product>();
            CreateMap<Product, ProductSummaryApiModel>();
            CreateMap<CreateProductCommand, Product>()
                .ForMember(d => d.ProductCategories, conf => conf.Ignore());
            CreateMap<Product, ProductDetailApiModel>()
                .ConstructUsing(p => new ProductDetailApiModel(p.RetailPrice, p.Name, p.Quantity,
                    p.CostPrice,p.SupplyDate));
        }
    }
}
