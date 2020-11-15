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
    public class GetInventoryDetailQuery : IRequest<InventoryDetailApiModel>
    {
        public int Id { get; set; }
    }

    public class GetInventoryDetailQueryHandler : IRequestHandler<GetInventoryDetailQuery, InventoryDetailApiModel>
    {
        private readonly IInventoryRepository _repo;

        public GetInventoryDetailQueryHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<InventoryDetailApiModel> Handle(GetInventoryDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetInventoryById(request.Id).ConfigureAwait(false);
            return result;
        }
    }
}
