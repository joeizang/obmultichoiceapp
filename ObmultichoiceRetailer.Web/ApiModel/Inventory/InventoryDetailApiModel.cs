using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.ApiModel.Category;
using ObmultichoiceRetailer.Web.ApiModel.Product;

namespace ObmultichoiceRetailer.Web.ApiModel.Inventory
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
