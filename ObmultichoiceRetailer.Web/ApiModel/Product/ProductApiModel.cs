using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.ApiModel.Category;

namespace ObmultichoiceRetailer.Web.ApiModel.Product
{
    public class ProductApiModel
    {
        public ProductApiModel(string name, float quantity, decimal suppliedPrice, decimal retailPrice, int id)
        {
            Name = name;
            Quantity = quantity;
            CostPrice = suppliedPrice;
            RetailPrice = retailPrice;
            ProductCategories = new List<CategoryApiModel>();
            Id = id;
        }

        public ProductApiModel()
        {
            ProductCategories = new List<CategoryApiModel>();
        }
        public string? Name { get; }

        public int Id { get; set; }

        public decimal RetailPrice { get; }

        public decimal CostPrice { get; }

        public float Quantity { get; }

        public List<CategoryApiModel> ProductCategories { get; }
    }

    public class ProductSummaryApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public float Quantity { get; set; }
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
        public ProductDetailApiModel(decimal retailPrice, string name, float quantity, decimal suppliedPrice, DateTimeOffset supplyDate, int id)
        {
            Name = name;
            RetailPrice = retailPrice;
            Quantity = quantity;
            CostPrice = suppliedPrice;
            ProductCategories = new List<CategoryApiModel>();
            SupplyDate = supplyDate;
            Id = id;
        }

        public ProductDetailApiModel()
        {
            ProductCategories = new List<CategoryApiModel>();
        }

        public int Id { get; set; }

        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public float Quantity { get; }

        public decimal CostPrice { get; }

        public List<CategoryApiModel> ProductCategories { get; }

        public DateTimeOffset SupplyDate { get; }
    }
}
