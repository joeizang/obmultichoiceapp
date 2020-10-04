using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Backend.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Backend.GraphQL
{
    public class Query
    {
        [UsePaging]
        [UseFiltering]
        [UseSelection]
        [UseSorting]
        public IQueryable<Sale> Sales([Service] RektaContext db) =>
            db.Sales.AsNoTracking();
    }
}
