using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.DomainEvents.Product;

namespace ObmultichoiceRetailer.Web.Commands.Product
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }



    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repo;
        private readonly IMediator _mediator;

        public DeleteProductCommandHandler(IProductRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _repo.DeleteProductAsync(request);
            await _repo.SaveAsync(cancellationToken);
            await _mediator.Publish(new ProductDeleteEvent {DeletedProductId = request.Id}, cancellationToken);
            return default;
        }
    }
}
