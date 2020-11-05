using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Web.Helpers
{
    public class PagedList<T> : List<T> where T : class
    {
        public int MaximumPageSize { get; } = 25;
        public int CurrentPage { get; }

        public int TotalPages { get; }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            //safety to ensure that no more than a max of 25 entities can be viewed at any time
            private set => _pageSize = (value > MaximumPageSize) ? MaximumPageSize : value;
        }

        public int TotalCount { get; private set; }

        public bool HasPrevious => (CurrentPage > 1);

        public bool HasNext => (CurrentPage < TotalPages);


        public PagedList(int count, int pageSize, int pageNumber, List<T> items)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public PagedList()
        {
            
        }

        public static async Task<PagedList<T>> CreatePagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync().ConfigureAwait(false);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);
            return new PagedList<T>(count,pageSize,pageNumber,items);
        }
    }
}
