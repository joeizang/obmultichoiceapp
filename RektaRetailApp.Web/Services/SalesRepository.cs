using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Services
{
    public class SalesRepository : GenericBaseRepository, ISalesRepository
    {
        public SalesRepository([NotNull] IHttpContextAccessor accessor, [NotNull] RektaContext db) : base(accessor, db)
        {
        }
    }
}
