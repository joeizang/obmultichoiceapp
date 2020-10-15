using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ApiModel.Category;
using RektaRetailApp.Web.ApiModel.Product;

namespace RektaRetailApp.Web.ApiModel.Inventory
{
    public class InventoryDetailApiModel
    {
        public InventoryDetailApiModel()
        {
            Categories = new List<CategoryApiModel>();
            InventoryItems = new List<ProductApiModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset InventoryDate { get; set; }

        public List<ProductApiModel> InventoryItems { get; set; }

        public List<CategoryApiModel> Categories { get; set; }
    }
}
