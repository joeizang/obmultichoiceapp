using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Product;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.DomainEvents.Product
{
    public class ProductCreateEvent : DomainEvent
    {
        public ProductCreateEvent(ProductDetailApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}
