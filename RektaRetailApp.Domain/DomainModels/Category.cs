using System.ComponentModel.DataAnnotations;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Category : BaseDomainModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string? Description { get; set; }
    }
}