using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.Data;

namespace ObmultichoiceRetailer.Web.Services
{
    public class SalesRepository : GenericBaseRepository, ISalesRepository
    {
        public SalesRepository([NotNull] IHttpContextAccessor accessor, [NotNull] ObmultichoiceContext db) : base(accessor, db)
        {
        }
    }
}
