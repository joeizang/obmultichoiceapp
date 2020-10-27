using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ApiModel.Supplier
{
    public class SupplierApiModel
    {
        public SupplierApiModel(string? name, string? mobileNumber, int supplierId)
        {
            Name = name;
            MobileNumber = mobileNumber;
            SupplierId = supplierId;
        }

        public string? Name { get; }

        public string? MobileNumber { get; }

        public int SupplierId { get; }
    }

    public class SupplierDetailApiModel
    {
        public SupplierDetailApiModel(string? name, string? phoneNumber, string? description)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            ProductIds = new List<int>();
        }

        public string? Name { get; }

        public string? PhoneNumber { get; }

        public string? Description { get; }

        public List<int> ProductIds { get; } 
    }
}
