using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Backend.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Backend.GraphQL.DataLoaders
{
    public class SalesDataLoader : DataLoaderBase<long, Sale>
    {
        private readonly RektaContext _db;


        public SalesDataLoader(RektaContext db)
        {
            _db = db;
        }


        protected override Task<IReadOnlyList<Result<Sale>>> FetchAsync(IReadOnlyList<long> keys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
