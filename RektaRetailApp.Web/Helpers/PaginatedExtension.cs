using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.Helpers
{
    public static class PaginatedExtension
    {
        public static Task<PagedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) 
            where TDestination : class 
            => PagedList<TDestination>.CreatePagedList(queryable, pageNumber, pageSize);
    }
}
