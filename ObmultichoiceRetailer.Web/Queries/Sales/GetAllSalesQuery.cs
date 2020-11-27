using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.Queries.Sales
{
    public class GetAllSalesQuery : IRequest<PaginatedResponse<SaleApiModel>>, IRequest<Response<SaleDetailApiModel>>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? SearchTerm { get; set; }

        public string? OrderByTerm { get; set; }
    }



    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery,PaginatedResponse<SaleApiModel>>
    {
        private readonly ISalesRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllSalesQueryHandler(ISalesRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }
        public async Task<PaginatedResponse<SaleApiModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sales = await _repo.GetAllSales(request, cancellationToken).ConfigureAwait(false);
                var prev = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber - 1).ToString()!);
                prev.AddQueryStringParams("pageSize", request.PageSize.ToString()!);
                var nextL = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber + 1).ToString()!);
                nextL.AddQueryStringParams("pageSize", request.PageSize.ToString()!);

                var prevLink = sales.HasPrevious
                    ? prev.GenerateUri() : null;
                var nextLink = sales.HasNext
                    ? nextL.GenerateUri() : null;

                var result = new PaginatedResponse<SaleApiModel>(sales,
                    sales.TotalCount, sales.PageSize, sales.CurrentPage,
                    prevLink?.PathAndQuery, nextLink?.PathAndQuery, ResponseStatus.Success);

                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<SaleApiModel>(new PagedList<SaleApiModel>(), 0, 10, 1, "","",ResponseStatus.Error, new { ErrorMessage = e.Message});
            }
        }
    }
}
