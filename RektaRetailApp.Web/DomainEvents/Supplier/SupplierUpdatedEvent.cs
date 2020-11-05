using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace RektaRetailApp.Web.DomainEvents.Supplier
{
    public class SupplierUpdatedEvent : INotification
    {
        public SupplierUpdatedEvent(string? name, string? mobileNumber, string? description)
        {
            Name = name;
            MobileNumber = mobileNumber;
            Description = description;
        }

        public string? Name { get; }

        public string? MobileNumber { get; }

        public string? Description { get; }
    }
}
