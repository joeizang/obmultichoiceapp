using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;

namespace ObmultichoiceRetailer.Web.Queries.Inventory
{
    public class GetInventoryDetailQuery : IRequest<Response<InventoryDetailApiModel>>
    {
        public int Id { get; set; }
    }

    public class GetInventoryDetailQueryHandler : IRequestHandler<GetInventoryDetailQuery, Response<InventoryDetailApiModel>>
    {
        private readonly IInventoryRepository _repo;

        public GetInventoryDetailQueryHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<InventoryDetailApiModel>> Handle(GetInventoryDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventory = await _repo.GetInventoryById(request.Id, cancellationToken).ConfigureAwait(false);

                var result = new Response<InventoryDetailApiModel>(inventory, ResponseStatus.Success);

                return result;
            }
            catch (Exception e)
            {
                return new Response<InventoryDetailApiModel>(new InventoryDetailApiModel(), ResponseStatus.Error,
                    new {ErrorMessage = e.Message});
            }
        }
    }
}
