using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;

namespace RektaRetailApp.Web.Commands.Inventory
{
  public class UpdateInventoryCommand : IRequest
  {
    public int InventoryId { get; set; }
    public string Name { get; set; } = null!;
    public string BatchNumber { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; } = null!;
  }

  public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Unit>
  {
    private readonly IInventoryRepository _repo;

    public UpdateInventoryCommandHandler(IInventoryRepository repo)
    {
      _repo = repo;
    }
    public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
      await _repo.UpdateInventory(request).ConfigureAwait(false);
      await _repo.SaveAsync().ConfigureAwait(false);
      return default;
    }
  }
}