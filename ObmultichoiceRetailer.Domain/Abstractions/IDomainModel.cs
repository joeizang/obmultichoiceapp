using System;
using System.Collections.Generic;
using System.Text;

namespace ObmultichoiceRetailer.Domain.Abstractions
{
    public interface IDomainModel
    {
        DateTimeOffset CreatedAt { get; set; }
        
        DateTimeOffset UpdatedAt { get; set; }

        int Id { get; set; }
    }
}
