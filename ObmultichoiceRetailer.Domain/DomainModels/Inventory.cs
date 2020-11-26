using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ObmultichoiceRetailer.Domain.Abstractions;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
    public class Inventory : BaseDomainModel
    {
        public Inventory()
        {
            InventoryItems = new List<Product>();
        }
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UnitMeasure UnitAmount { get; set; }

        public float Quantity { get; set; }

        public decimal TotalCostValue { get; private set; }

        public decimal TotalRetailValue { get; private set; }

        public List<Product> InventoryItems { get; set; }

        [Required]
        public DateTime SupplyDate { get; set; }

        public void CalculateTotalValuesOfInventory()
        {
            if (InventoryItems.Any())
            {
                TotalCostValue = InventoryItems.Sum(x => x.CostPrice);
                TotalRetailValue = InventoryItems.Sum(x => x.RetailPrice);

            }
            TotalCostValue = 0;
            TotalRetailValue = 0;
        }

    }
}
