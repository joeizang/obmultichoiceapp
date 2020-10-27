using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IDomainEvent : INotification
    {
        public DateTimeOffset HappenedAt { get; }
    }
}
