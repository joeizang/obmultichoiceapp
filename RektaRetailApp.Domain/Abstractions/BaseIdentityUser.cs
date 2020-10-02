using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RektaRetailApp.Domain.Abstractions
{
    public class BaseIdentityUser : IdentityUser<long>, IDomainModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
