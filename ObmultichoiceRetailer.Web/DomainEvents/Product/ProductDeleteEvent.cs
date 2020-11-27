using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.DomainEvents.Product
{
    public class ProductDeleteEvent : DomainEvent
    {
        public int DeletedProductId { get; set; }

        public ProductDeleteEvent()
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Deletion;
        }

    }
}
