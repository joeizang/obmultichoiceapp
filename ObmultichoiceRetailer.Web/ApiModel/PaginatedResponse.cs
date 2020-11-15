using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.ApiModel
{
    public class PaginatedResponse<T> where T : class
    {
        public PagedList<T> List { get; }

        public int TotalCount { get; }
        public int PageSize { get; }

        public int CurrentPage { get; }

        public string? PreviousPageLink { get; }

        public string? NextPageLink { get; }


        public PaginatedResponse(PagedList<T> list, int totalCount, int pageSize, int currentPage, string? previousPageLink, string? nextPageLink)
        {
            List = list;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
        }

        public PaginatedResponse(PagedList<T> list)
        {
            List = list;
        }

        public PaginatedResponse()
        {
            List = new PagedList<T>();
        }
    }
}
