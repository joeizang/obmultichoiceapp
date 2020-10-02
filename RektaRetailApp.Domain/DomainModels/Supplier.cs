using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Supplier : BaseDomainModel
    {
        public Supplier()
        {
            ProductsSupplied = new List<Item>();
        }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string MobileNumber { get; set; } = null!;
        public List<Item> ProductsSupplied { get; set; }
    }
}