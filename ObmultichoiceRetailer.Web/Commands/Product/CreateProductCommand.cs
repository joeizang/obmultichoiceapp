using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Product;
using ObmultichoiceRetailer.Web.DomainEvents.Product;

namespace ObmultichoiceRetailer.Web.Commands.Product
{
    public class CreateProductCommand : IRequest<Response<ProductDetailApiModel>>
    {
        public string Name { get; set; } = null!;

        public decimal RetailPrice { get; set; }

        public float Quantity { get; set; }

        public decimal CostPrice { get; set; }

        public string? Brand { get; set; }

        public string? Comments { get; set; }

        public DateTimeOffset SupplyDate { get; set; }

        public int InventoryId { get; set; }

        public UnitMeasure UnitMeasure { get; set; }

        public bool Verified { get; set; }

    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDetailApiModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(IProductRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }
        public async Task<Response<ProductDetailApiModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
                {
                    p => p.ProductCategories
                };
                await _repo.CreateProductAsync(request, cancellationToken).ConfigureAwait(false);

                await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);

                var product = await _repo.GetProductByAsync(cancellationToken, includes, p => p.Name.Equals(request.Name.ToUpperInvariant()),
                    p => p.RetailPrice == request.RetailPrice, p => p.CostPrice == request.CostPrice);

                var model = new ProductDetailApiModel(product.RetailPrice, product.Name, product.Quantity,
                    product.CostPrice, product.SupplyDate, product.Id);
                var result = new Response<ProductDetailApiModel>(model, ResponseStatus.Success);
                var createEvent = new ProductCreateEvent(model);
                await _mediator.Publish(createEvent, cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return new Response<ProductDetailApiModel>(new ProductDetailApiModel(), ResponseStatus.Failure, new
                {
                    e.Message,
                    Time = DateTimeOffset.Now
                });
            }
        }
    }
}
