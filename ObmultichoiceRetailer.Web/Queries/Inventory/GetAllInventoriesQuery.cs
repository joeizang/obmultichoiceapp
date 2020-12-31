using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;
using ObmultichoiceRetailer.Web.Helpers;

namespace ObmultichoiceRetailer.Web.Queries.Inventory
{
    public class GetAllInventoriesQuery : IRequest<PaginatedResponse<InventoryApiModel>>
    // public class GetAllInventoriesQuery : IRequest<IEnumerable<InventoryApiModel>>
    {
        public string? SearchString { get; set; }

        public bool Ascending { get; set; } = true;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; } = "Date";
    }


    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, PaginatedResponse<InventoryApiModel>>
    // public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryApiModel>>
    {
        private readonly IInventoryRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllInventoryQueryHandler(IInventoryRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }

        public async Task<PaginatedResponse<InventoryApiModel>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        // public async Task<IEnumerable<InventoryApiModel>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventories = await _repo.GetAllInventories(request, cancellationToken)
                        .ConfigureAwait(false);
                var prev = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber - 1).ToString()!);
                prev.AddQueryStringParams("pageSize", request.PageSize.ToString()!);
                var nextL = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber + 1).ToString()!);
                nextL.AddQueryStringParams("pageSize", request.PageSize.ToString()!);
                var pagedList = PagedList<InventoryApiModel>.CreatePagedList(inventories.AsQueryable(),
                    request.PageNumber,request.PageSize);

                var prevLink = pagedList.HasPrevious ? prev.GenerateUri() : null;
                var nextLink = pagedList.HasNext ? nextL.GenerateUri() : null;
                var result = new PaginatedResponse<InventoryApiModel>(pagedList, pagedList.TotalCount,
                    pagedList.PageSize, pagedList.CurrentPage, prevLink?.AbsoluteUri, nextLink?.AbsoluteUri, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<InventoryApiModel>(new PagedList<InventoryApiModel>(), 0,10,1,"","",ResponseStatus.Error, 
                    new { ErrorMessage = e.Message});
                // return new List<InventoryApiModel>();
            }
        }
    }
}
