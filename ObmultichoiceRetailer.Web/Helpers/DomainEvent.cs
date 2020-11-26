using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions;

namespace ObmultichoiceRetailer.Web.Helpers
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime HappenedAt { get; set; }

        public string ActionPerformed { get; set; } = null!;
        public object? PayLoad { get; set; }
    }
}
