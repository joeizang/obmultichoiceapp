using System;
using System.Collections.Generic;
using System.Text;

namespace ObmultichoiceRetailer.Domain.Abstractions
{
    public interface IDomainModel
    {
        DateTime CreatedAt { get; set; }
        
        DateTime UpdatedAt { get; set; }

        int Id { get; set; }
    }
}
