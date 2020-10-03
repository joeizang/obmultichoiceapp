using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ItemSold : BaseDomainModel
    {

        [Required]
        public string ItemName { get; set; } = null!;

        [Required]
        public float Quantity { get; set; }

        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }

    }
}
