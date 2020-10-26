using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Inventory;

namespace RektaRetailApp.Web.Commands.Inventory
{
  public class CreateInventoryCommand : IRequest<InventoryApiModel>
  {
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? BatchNumber { get; set; }

    public string CategoryName { get; set; } = null!;

    public float ProductQuantity { get; set; }

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
      await _repo.SaveAsync().ConfigureAwait(false);
      var result = await _repo
          .GetInventoryBy(x =>
              x.BatchNumber!.Equals(request.BatchNumber!.ToUpperInvariant()) &&
              x.Name!.Equals(request.Name.ToUpperInvariant()) &&
              x.Description!.Equals(request.Description!.ToUpperInvariant()));
      return result;
    }
  }
}
