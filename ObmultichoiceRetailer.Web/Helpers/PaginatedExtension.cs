using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObmultichoiceRetailer.Web.Helpers
{
    public static class PaginatedExtension
    {
        public static Task<PagedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) 
            where TDestination : class 
            => PagedList<TDestination>.CreatePagedList(queryable, pageNumber, pageSize);

        public static PagedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            where TDestination : class
            => new PagedList<TDestination>(queryable.Count(),pageSize,pageNumber,queryable.ToList());
    }
}
