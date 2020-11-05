using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Category;
using RektaRetailApp.Web.ApiModel.Product;

namespace RektaRetailApp.Web.Commands.Product
{
    public class CreateProductCommand : IRequest<Response<ProductDetailApiModel>>
    {
        public string Name { get; set; } = null!;

        public decimal RetailPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public float Quantity { get; set; }

        public decimal SuppliedPrice { get; set; }

        public DateTimeOffset SupplyDate { get; set; }

        public int InventoryId { get; set; }

        public int SupplierId { get; set; }

    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDetailApiModel>>
    {
        private readonly IProductRepository _repo;

        public CreateProductCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<ProductDetailApiModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
                {
                    p => p.ProductCategories,
                    p => p.ProductSupplier
                };
                await _repo.CreateProductAsync(request).ConfigureAwait(false);
                await _repo.SaveAsync().ConfigureAwait(false);      
                var product = await _repo.GetProductByAsync(includes, p => p.Name.Equals(request.Name.ToUpperInvariant()),
                    p => p.RetailPrice == request.RetailPrice, p => p.SuppliedPrice == request.SuppliedPrice,
                    p => p.SupplierId == request.SupplierId);

                //var model = _mapper.Map<Domain.DomainModels.Product, ProductDetailApiModel>(created);
                var model = new ProductDetailApiModel(product.RetailPrice, product.UnitPrice, product.Name, product.Quantity,
                    product.SuppliedPrice, product.ProductSupplier.Name, product.ProductSupplier.MobileNumber, product.SupplyDate);
                var result = new Response<ProductDetailApiModel>(model, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<ProductDetailApiModel>(new ProductDetailApiModel(), ResponseStatus.Failure, new
                {
                    e.Message,
                    Time = DateTimeOffset.Now.LocalDateTime
                });
            }
        }
    }
}
