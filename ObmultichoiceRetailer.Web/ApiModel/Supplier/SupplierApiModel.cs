using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.ApiModel.Product;

namespace ObmultichoiceRetailer.Web.ApiModel.Supplier
{
    public class SupplierApiModel
    {
        public SupplierApiModel(string? name, string? mobileNumber, int supplierId)
        {
            Name = name;
            MobileNumber = mobileNumber;
            SupplierId = supplierId;
        }

        public SupplierApiModel()
        {
            
        }

        public string? Name { get; }

        public string? MobileNumber { get; }

        public int SupplierId { get; }
    }

    public class SupplierDetailApiModel
    {
        public SupplierDetailApiModel(string? name, string? phoneNumber, string? description, int id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            SupplierId = id;
        }

        public int SupplierId { get; }
        public string? Name { get; }

        public string? PhoneNumber { get; }

        public string? Description { get; }

        public List<ProductSummaryApiModel> ProductsSupplied { get; set; } = new List<ProductSummaryApiModel>();

    }
}
