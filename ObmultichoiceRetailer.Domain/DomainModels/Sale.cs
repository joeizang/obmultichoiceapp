using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ObmultichoiceRetailer.Domain.Abstractions;

namespace ObmultichoiceRetailer.Domain.DomainModels
{
  public class Sale : BaseDomainModel
  {
    public Sale()
    {
      ItemsSold = new List<Product>();
    }
    public DateTime SaleDate { get; set; }

    public string SalesPerson { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal GrandTotal { get; set; }

    public SaleType TypeOfSale { get; set; }

    public PaymentType ModeOfPayment { get; set; }

    public List<Product> ItemsSold { get; set; }
  }
}
