using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Category : BaseDomainModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}