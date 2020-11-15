using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ObmultichoiceRetailer.Web.Abstractions
{
    public interface IDomainEvent : INotification
    {
        DateTimeOffset HappenedAt { get; }

        string ActionPerformed { get; }

        object? PayLoad { get; }
    }
}
