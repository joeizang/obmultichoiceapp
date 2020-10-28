using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.ApiModel
{
    public class PaginatedResponse<T> where T : class
    {
        public PagedList<T> List { get; }
        public PaginatedMetaData Data { get; }


        public PaginatedResponse(PagedList<T> list, PaginatedMetaData data)
        {
            List = list;
            Data = data;
        }
    }
}
