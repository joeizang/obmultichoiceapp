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
            Items = new List<Item>();
        }
        public DateTimeOffset SaleDate { get; set; }

        [ForeignKey(nameof(SalesPerson))]
        public long SalesPersonId { get; set; }

        public ApplicationUser SalesPerson { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }

        public SaleType SaleType { get; set; }

        public Customer Customer { get; set; } = null!;

        public List<Item> Items { get; set; }
    }
}
