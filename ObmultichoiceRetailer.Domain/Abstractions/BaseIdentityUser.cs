using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ObmultichoiceRetailer.Domain.Abstractions
{
    public class BaseIdentityUser : IdentityUser<int>, IDomainModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
