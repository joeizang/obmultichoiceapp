using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
  public class Sale : BaseDomainModel
  {
    public Sale()
    {
      ItemsSold = new List<ItemSold>();
    }
    public DateTimeOffset SaleDate { get; set; }

    [ForeignKey(nameof(SalesPerson))]
    public string SalesPersonId { get; set; } = null!;

    public ApplicationUser SalesPerson { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal GrandTotal { get; set; }

    public SaleType TypeOfSale { get; set; }

    public PaymentType ModeOfPayment { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public List<ItemSold> ItemsSold { get; set; }
  }
}
