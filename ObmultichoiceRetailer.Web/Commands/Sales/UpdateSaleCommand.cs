using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Sales;

namespace ObmultichoiceRetailer.Web.Commands.Sales
{
    public class UpdateSaleCommand : IRequest<Response<SaleDetailApiModel>>
    {
    }



    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Response<SaleDetailApiModel>>
    {
        public Task<Response<SaleDetailApiModel>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
