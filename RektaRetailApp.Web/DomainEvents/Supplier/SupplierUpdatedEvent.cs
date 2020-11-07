using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.DomainEvents.Supplier
{
    public class SupplierUpdatedEvent : DomainEvent
    {
        public SupplierUpdatedEvent(string? name, string? mobileNumber, string? description)
        {
            Name = name;
            MobileNumber = mobileNumber;
            Description = description;
            ActionPerformed = TaskPerformed.Modification;
        }

        public string? Name { get; }

        public string? MobileNumber { get; }

        public string? Description { get; }

        
    }
}
