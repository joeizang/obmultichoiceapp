using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.ApiModel.Supplier;

namespace RektaRetailApp.Web.DomainEvents.Supplier
{
    public class SupplierCreatedEvent : IDomainEvent
    {

        public SupplierCreatedEvent(SupplierApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            PayLoad = model;
        }
        public DateTimeOffset HappenedAt { get; }

        public SupplierApiModel PayLoad { get; }
    }
}
