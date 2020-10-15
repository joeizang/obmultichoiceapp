using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ApiModel.Inventory
{
    public class InventoryApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public int ProductId { get; set; }

        public string CategoryName { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
