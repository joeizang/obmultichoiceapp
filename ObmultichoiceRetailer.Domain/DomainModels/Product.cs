using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ObmultichoiceRetailer.Domain.Abstractions;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
  public class Product : BaseDomainModel
  {
    public Product()
    {
      ProductCategories = new List<Category>();
    }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(12,2)")]
    [Required]
    public decimal RetailPrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(12,2)")]
    public decimal CostPrice { get; set; }

    [Required]
    public DateTime SupplyDate { get; set; }

    [Required]
    public float Quantity { get; set; }

    public string? Brand { get; set; }

    public string? Comments { get; set; }

    public UnitMeasure UnitMeasure { get; set; }

    public bool Verified { get; set; }

    public List<Category> ProductCategories { get; set; }

    public Inventory? Inventory { get; set; }

    public int InventoryId { get; set; }

  }
}