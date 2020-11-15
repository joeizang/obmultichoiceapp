using System.ComponentModel.DataAnnotations;
using ObmultichoiceRetailer.Domain.Abstractions;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
    public class Category : BaseDomainModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }
    }
}