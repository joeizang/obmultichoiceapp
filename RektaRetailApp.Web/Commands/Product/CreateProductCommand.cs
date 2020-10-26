using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Category;
using RektaRetailApp.Web.ApiModel.Product;

namespace RektaRetailApp.Web.Commands.Product
{
    public class CreateProductCommand : IRequest<ProductApiModel>
    {
        public CreateProductCommand()
        {
            ProductCategories = new List<CategoryApiModel>();
        }

        public string Name { get; set; } = null!;

        public decimal RetailPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public float Quantity { get; set; }

        public decimal SuppliedPrice { get; set; }

        public List<CategoryApiModel> ProductCategories { get; set; }

        public int SupplierId { get; set; }

    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductApiModel>
    {
        private readonly IProductRepository _repo;

        public CreateProductCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<ProductApiModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.CreateProductAsync(request).ConfigureAwait(false);
            return result;
        }
    }
}
