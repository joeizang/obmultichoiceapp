using System;
using System.Collections.Generic;
using System.Text;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class SuppliersInventories : BaseDomainModel
    {
        public long InventoryId { get; set; }

        public Inventory ProductInventory { get; set; } = null!;

        public long SupplierId { get; set; }

        public Supplier ProductSupplier { get; set; } = null!;

    }
}
