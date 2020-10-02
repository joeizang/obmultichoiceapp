using System;
using System.Collections.Generic;
using System.Text;

namespace RektaRetailApp.Domain.Abstractions
{
    public interface IDomainModel
    {
        DateTimeOffset CreatedAt { get; set; }
        
        DateTimeOffset UpdatedAt { get; set; }

        long Id { get; set; }
    }
}
