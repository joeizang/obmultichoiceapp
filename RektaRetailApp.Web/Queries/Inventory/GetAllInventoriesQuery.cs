using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Inventory;

namespace RektaRetailApp.Web.Queries.Inventory
{
    public class GetAllInventoriesQuery : IRequest<IEnumerable<InventoryApiModel>>
    {
        public string? SearchString { get; set; }

        public bool Ascending { get; set; } = true;

    }


    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryApiModel>>
    {
        private readonly IInventoryRepository _repo;
        private readonly IMapper _mapper;

        public GetAllInventoryQueryHandler(IInventoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryApiModel>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllInventories(request.SearchString, request.Ascending)
                .ConfigureAwait(false);
            return result;
        }
    }
}
