using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ApiModel
{
    public class Response<T> where T : class
    {
        public T Data { get; } = null!;

        public Response()
        {
            
        }

        public Response(T data)
        {
            Data = data;
        }
    }
}
