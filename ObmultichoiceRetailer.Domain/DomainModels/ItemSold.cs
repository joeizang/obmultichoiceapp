using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ObmultichoiceRetailer.Domain.Abstractions;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
  public class ItemSold : BaseDomainModel
  {

    [Required]
    public string ItemName { get; set; } = null!;

    [Required]
    public float Quantity { get; set; }

    public string? Comments { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    [Required]
    public decimal Price { get; set; }

    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

  }
}
