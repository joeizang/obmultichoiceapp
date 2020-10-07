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

        [StringLength(450)]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(9,2)")]
        [Required]
        public decimal Price { get; set; }

        public Category ItemSoldCategory { get; set; } = null!;

        [ForeignKey(nameof(ItemSoldCategory))]
        public long ItemSoldCategoryId { get; set; }

        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}
