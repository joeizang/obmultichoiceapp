using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.Queries.Supplier
{
  public class GetAllSuppliersQuery : IRequest<PaginatedResponse<SupplierApiModel>>
  {
    public string? SearchTerm { get; set; }

    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }

    public string? UriName { get; set; }
  }

  public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PaginatedResponse<SupplierApiModel>>
  {
    private readonly ISupplierRepository _repo;
    private readonly IUriGenerator _ugen;

    public GetAllSuppliersQueryHandler(ISupplierRepository repo, IUriGenerator ugen)
    {
      _repo = repo;
      _ugen = ugen;
    }
    public async Task<PaginatedResponse<SupplierApiModel>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
      var pagedResult = await _repo.GetSuppliersAsync(request).ConfigureAwait(false);

      var prev = _ugen.AddQueryStringParams("pageNumber", (request.PageNumber - 1).ToString()!);
      prev.AddQueryStringParams("pageSize", request.PageSize.ToString()!);
      var nextL = _ugen.AddQueryStringParams("pageNumber", (request.PageNumber + 1).ToString()!);
      nextL.AddQueryStringParams("pageSize", request.PageSize.ToString()!);

      var prevLink = pagedResult.HasPrevious
          ? prev.GenerateUri() : null;
      var nextLink = pagedResult.HasNext
          ? nextL.GenerateUri() : null;


      var result = new PaginatedResponse<SupplierApiModel>(pagedResult,
          pagedResult.TotalCount, pagedResult.PageSize, pagedResult.CurrentPage, prevLink?.AbsoluteUri, nextLink?.AbsoluteUri);
      return result;
    }
  }
}
