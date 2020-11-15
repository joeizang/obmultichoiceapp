using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;

namespace ObmultichoiceRetailer.Web.Queries.Inventory
{
    public class GetAllInventoriesQuery : IRequest<IEnumerable<InventoryApiModel>>
    {
        public string? SearchString { get; set; }

        public bool Ascending { get; set; } = true;

    }


    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryApiModel>>
    {
        private readonly IInventoryRepository _repo;

        public GetAllInventoryQueryHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<InventoryApiModel>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllInventories(request.SearchString, request.Ascending)
                .ConfigureAwait(false);
            return result;
        }
    }
}
