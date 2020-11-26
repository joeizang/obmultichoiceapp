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
using ObmultichoiceRetailer.Web.ApiModel.Inventory;

namespace ObmultichoiceRetailer.Web.Commands.Inventory
{
  public class CreateInventoryCommand : IRequest<InventoryApiModel>
  {
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float Quantity { get; set; }

    public DateTime SupplyDate { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public UnitMeasure UnitMeasure { get; set; }

  }


  public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, InventoryApiModel>
  {
    private readonly IInventoryRepository _repo;

    public CreateInventoryCommandHandler(IInventoryRepository repo)
    {
      _repo = repo;
    }
    public async Task<InventoryApiModel> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
      _repo.CreateInventory(request);
      await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
      var result = await _repo
          .GetInventoryBy(x =>
              x.Name!.Equals(request.Name.ToUpperInvariant()) &&
              x.Description!.Equals(request.Description!.ToUpperInvariant()));
      return result;
    }
  }
}
