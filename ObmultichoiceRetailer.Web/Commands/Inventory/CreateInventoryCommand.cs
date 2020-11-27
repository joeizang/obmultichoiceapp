using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;

namespace ObmultichoiceRetailer.Web.Commands.Inventory
{
    public class CreateInventoryCommand : IRequest<Response<InventoryApiModel>>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public float Quantity { get; set; }

        public DateTimeOffset SupplyDate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UnitMeasure UnitMeasure { get; set; }

    }


    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Response<InventoryApiModel>>
    {
        private readonly IInventoryRepository _repo;

        public CreateInventoryCommandHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<InventoryApiModel>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _repo.CreateInventory(request);
                await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
                var inventory = await _repo
                    .GetInventoryBy(x =>
                        x.Name!.Equals(request.Name.ToUpperInvariant()) &&
                        x.Description!.Equals(request.Description!.ToUpperInvariant()));
                var result = new Response<InventoryApiModel>(inventory, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<InventoryApiModel>(new InventoryApiModel(), ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }
}
