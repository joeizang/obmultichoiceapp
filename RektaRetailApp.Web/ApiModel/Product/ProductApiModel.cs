using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ApiModel.Category;

namespace RektaRetailApp.Web.ApiModel.Product
{
    public class ProductApiModel
    {
        public ProductApiModel(int supplierId, float quantity, decimal suppliedPrice, decimal unitPrice, decimal retailPrice)
        {
            SupplierId = supplierId;
            Quantity = quantity;
            SuppliedPrice = suppliedPrice;
            UnitPrice = unitPrice;
            RetailPrice = retailPrice;
            ProductCategories = new List<CategoryApiModel>();
        }
        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public decimal UnitPrice { get; }

        public decimal SuppliedPrice { get; }

        public float Quantity { get; }

        public List<CategoryApiModel> ProductCategories { get; }

        public int SupplierId { get; }
    }

    public class CreateProductApiModel
    {
        public CreateProductApiModel(int supplierId, decimal suppliedPrice, float quantity, decimal unitPrice, decimal retailPrice, DateTimeOffset supplyDate)
        {
            SupplierId = supplierId;
            SuppliedPrice = suppliedPrice;
            Quantity = quantity;
            UnitPrice = unitPrice;
            RetailPrice = retailPrice;
            SupplyDate = supplyDate;
            ProductCategories = new List<CategoryApiModel>();
        }

        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public decimal UnitPrice { get; }

        public float Quantity { get; }

        public decimal SuppliedPrice { get; }

        public List<CategoryApiModel> ProductCategories { get; }

        public int SupplierId { get; }

        public DateTimeOffset SupplyDate { get; }
    }

    public class ProductDetailApiModel
    {
        public ProductDetailApiModel(decimal retailPrice, decimal unitPrice, 
            float quantity, decimal suppliedPrice, string supplierName, string mobileNumber, DateTimeOffset supplyDate)
        {
            RetailPrice = retailPrice;
            UnitPrice = unitPrice;
            Quantity = quantity;
            SuppliedPrice = suppliedPrice;
            ProductCategories = new List<CategoryApiModel>();
            SupplierName = supplierName;
            MobileNumber = mobileNumber;
            SupplyDate = supplyDate;
        }

        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public decimal UnitPrice { get; }

        public float Quantity { get; }

        public decimal SuppliedPrice { get; }

        public List<CategoryApiModel> ProductCategories { get; }

        public string SupplierName { get; }

        public string MobileNumber { get; }

        public DateTimeOffset SupplyDate { get; }
    }
}
