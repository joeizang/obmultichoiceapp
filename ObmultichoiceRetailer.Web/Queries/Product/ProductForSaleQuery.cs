using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Product;
using ObmultichoiceRetailer.Web.Data;

namespace ObmultichoiceRetailer.Web.Queries.Product
{
    public class GetProductsForSaleQuery : IRequest<Response<IEnumerable<ProductsForSaleApiModel>>>
    {
    }


    public class GetProductsForSaleQueryHandler : IRequestHandler<GetProductsForSaleQuery, Response<IEnumerable<ProductsForSaleApiModel>>>
    {
        private readonly ObmultichoiceContext _db;

        public GetProductsForSaleQueryHandler(ObmultichoiceContext db)
        {
            _db = db;
        }
        public async Task<Response<IEnumerable<ProductsForSaleApiModel>>> Handle(GetProductsForSaleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var work = await _db.Products.AsNoTracking()
                    .Select(p => new ProductsForSaleApiModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.RetailPrice
                    }).ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
                var result = new Response<IEnumerable<ProductsForSaleApiModel>>(work, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ProductsForSaleApiModel>>(
                    new List<ProductsForSaleApiModel>(), ResponseStatus.Error, new { ErrorMessage = e.Message} );
            }
        }
    }
}
