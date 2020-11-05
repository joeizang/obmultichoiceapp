using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ApiModel
{
    public class Response<T> where T : class
    {
        public T Data { get; } = null!;

        public dynamic? Errors { get; }

        public string CurrentResponseStatus { get; }

        public Response(string currentResponseStatus, dynamic errors)
        {
            CurrentResponseStatus = currentResponseStatus;
            Errors = errors;
        }

        public Response(T data, string currentResponseStatus, dynamic errors = null!)
        {
            Data = data ?? throw new ArgumentException("the first argument is in an invalid state!");
            CurrentResponseStatus = currentResponseStatus;
            if (errors != null && CurrentResponseStatus != ResponseStatus.Success)
                Errors = errors;
            CurrentResponseStatus = ResponseStatus.Failure;
            Errors = new
            {
                ErrorMessage = "The response is assuming an invalid state and has been defaulted to a state of fault!"
            };
        }
    }

    public class ResponseStatus
    {
        public static string Success { get; } = nameof(Success);

        public static string Error { get; set; } = nameof(Error);

        public static string Failure { get; set; } = nameof(Failure);
    }
}
