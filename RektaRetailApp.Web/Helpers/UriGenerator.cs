using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace RektaRetailApp.Web.Helpers
{
  public interface IUriGenerator
  {
    UriGenerator AddQueryStringParams(string segmentName, string segmentValue);
    Uri GenerateUri();
    string BaseUri { get; }
  }

  public class UriGenerator : IUriGenerator
  {
    private readonly IHttpContextAccessor? _accessor;
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
      _accessor = accessor;
      var baseUri =
          $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host.ToUriComponent()}";
      BaseUri = baseUri ?? throw new ArgumentException("Looks like you failed to provide a valid context accessor!");
    }

    private UriGenerator() { }
  }
}
