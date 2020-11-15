using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace ObmultichoiceRetailer.Web.Helpers
{
    public interface IUriGenerator
    {
        UriGenerator AddQueryStringParams(string segmentName, string segmentValue);
        Uri GenerateUri();
        string BaseUri { get; }
    }

    public class UriGenerator : IUriGenerator
    {
        public string BaseUri { get; } = null!;
        private string _generatedUri = string.Empty;
        public UriGenerator AddQueryStringParams(string segmentName, string segmentValue)
        {
            _generatedUri = QueryHelpers.AddQueryString(BaseUri, segmentName, segmentValue);
            return this;
        }

        public Uri GenerateUri()
        {
            return new Uri(_generatedUri);
        }

        public UriGenerator(IHttpContextAccessor accessor)
        {
            var accessor1 = accessor;
            var baseUri =
                $"{accessor1.HttpContext?.Request.Scheme}://{accessor1.HttpContext?.Request.Host.ToUriComponent()}";
            BaseUri = baseUri ?? throw new ArgumentException("Looks like you failed to provide a valid context accessor!");
        }

        private UriGenerator() { }
    }
}
