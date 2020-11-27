using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.ApiModel
{
    public class PaginatedResponse<T> where T : class
    {
        public PagedList<T> List { get; }

        public int TotalCount { get; }
        public int PageSize { get; }

        public int CurrentPage { get; }

        public string? PreviousPageLink { get; private set; }

        public string? NextPageLink { get; private set; }

        public string CurrentResponseStatus { get; }

        public List<object>? Error { get; set; }

        public void SetNavLinks(string? previousLink, string? nextLink)
        {
            if (string.IsNullOrEmpty(previousLink) || string.IsNullOrEmpty(nextLink))
            {
                PreviousPageLink = string.Empty;
                NextPageLink = string.Empty;
            }
            else
            {
                PreviousPageLink = previousLink;
                NextPageLink = nextLink;
            }
        }


        public PaginatedResponse(PagedList<T> list, int totalCount, int pageSize, int currentPage, string? previousPageLink, 
            string? nextPageLink, string currentResponseStatus, object? errors = null)
        {
            List = list;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
            CurrentResponseStatus = currentResponseStatus;
            if(errors != null)
                Error?.Add(errors);
        }

        public PaginatedResponse(PagedList<T> list, string currentResponseStatus)
        {
            List = list;
            CurrentResponseStatus = currentResponseStatus;
        }

        public PaginatedResponse()
        {
            List = new PagedList<T>();
            CurrentResponseStatus = ResponseStatus.NonAction;
        }
    }
}
