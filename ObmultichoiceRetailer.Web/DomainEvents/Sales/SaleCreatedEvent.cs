using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.DomainEvents.Sales
{
    public class SaleCreatedEvent : DomainEvent
    {
        public SaleCreatedEvent(SaleApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}
