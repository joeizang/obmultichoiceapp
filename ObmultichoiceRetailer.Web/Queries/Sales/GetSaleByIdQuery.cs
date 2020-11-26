using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Sales;

namespace ObmultichoiceRetailer.Web.Queries.Sales
{
    public class GetSaleByIdQuery : IRequest<Response<SaleDetailApiModel>>
    {
        public int Id { get; set; }
    }


    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Response<SaleDetailApiModel>>
    {
        private readonly ISalesRepository _repo;

        public GetSaleByIdQueryHandler(ISalesRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<SaleDetailApiModel>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _repo.GetSaleById(request, cancellationToken).ConfigureAwait(false);
            var result = new Response<SaleDetailApiModel>(
                new SaleDetailApiModel(sale.Id, sale.SalesPerson, sale.SaleDate, sale.TypeOfSale, sale.ModeOfPayment),
                ResponseStatus.Success);
            return result;
        }
    }
}
