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
        public string Name { get; set; }
        public string BatchNumber { get; set; }
        public string CategoryName { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public DateTimeOffset SupplyDate { get; set; }
    }
    
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Unit>
    {
        private readonly IInventoryRepository _repo;

        public UpdateInventoryCommandHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var target = _repo.GetInventoryById(request.InventoryId);
            return default;
        }
    }
}