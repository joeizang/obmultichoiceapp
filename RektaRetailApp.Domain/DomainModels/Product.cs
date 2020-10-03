using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Product : BaseDomainModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(9,2)")]
        public decimal RetailPrice { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal SuppliedPrice { get; set; }

        [ForeignKey(nameof(ItemCategory))]
        public long CategoryId { get; set; }

        public Category ItemCategory { get; set; } = null!; // set a default category for all the items in stock
    }
}