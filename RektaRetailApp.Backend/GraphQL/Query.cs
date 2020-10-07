using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Backend.Data;
using RektaRetailApp.Backend.Extensions;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Backend.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering]
        [UseSelection]
        [UseSorting]
        public IQueryable<Sale> Sales([ScopedService] RektaContext db) =>
            db.Sales.AsNoTracking();


        [UseApplicationDbContext]
        [UsePaging]
        //[UseFiltering]
        [UseSelection]
        [UseSorting]
        public IQueryable<Inventory> Inventories([ScopedService] RektaContext db) =>
            db.Inventories.AsNoTracking();
    }
}
