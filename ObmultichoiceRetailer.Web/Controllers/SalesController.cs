using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Commands.Sales;
using ObmultichoiceRetailer.Web.Queries.Sales;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObmultichoiceRetailer.Web.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<SalesController>
        [HttpGet(Name="GetAllSales")]
        public async Task<ActionResult<PaginatedResponse<SaleApiModel>>> Get([FromQuery]GetAllSalesQuery query)
        {
            var result = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}", Name = "GetSaleById")]
        public async Task<ActionResult<Response<SaleDetailApiModel>>> Get([FromQuery]GetSaleByIdQuery query)
        {
            var result = await _mediator.Send(query).ConfigureAwait(false);
            if (result.CurrentResponseStatus.Equals(ResponseStatus.Error))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST api/<SalesController>
        [HttpPost]
        public async Task<ActionResult<Response<SaleApiModel>>> Post([FromBody] CreateSaleCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return CreatedAtRoute("GetSaleById", new { result.Data.Id }, result);
        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SalesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
