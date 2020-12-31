using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ObmultichoiceRetailer.Web.Helpers
{
    public static class PaginatedExtension
    {
        public static Task<PagedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize,
            CancellationToken token) where TDestination : class 
            => PagedList<TDestination>.CreatePagedListAsync(queryable, pageNumber, pageSize, token);

        public static PagedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            where TDestination : class
            => new PagedList<TDestination>(queryable.Count(),pageSize,pageNumber,queryable.ToList());
    }
}
