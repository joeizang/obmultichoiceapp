using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace RektaRetailApp.Web.Helpers
{
    public class DomainEvent : INotification
    {
        public DateTimeOffset HappenedAt { get; set; }

        public string ActionPerformed { get; set; } = null!;
    }
}
